#!/usr/bin/env python3
"""
Find missing es-ES translations for entity name and description fields.

Scans ALL YAML prototype files for entities with name/description, then checks
es-ES locale files for two types of gaps:

  1. Missing name  — ent-{id} key is completely absent from es-ES
  2. Missing desc  — ent-{id} key exists but has no .desc attribute, yet the
                     entity has a description (own or inherited from parent)

Handles prototype inheritance: child entities without their own name/description
resolve it by walking up the parent chain.

Usage:
    python Tools/find_missing_es_translations.py
    python Tools/find_missing_es_translations.py --output report.ftl
"""

import argparse
import re
import sys
from collections import defaultdict
from pathlib import Path

ROOT = Path(__file__).parent.parent
PROTOTYPES_DIR = ROOT / "Resources" / "Prototypes"
LOCALE_ES_DIR = ROOT / "Resources" / "Locale" / "es-ES"
DEFAULT_OUTPUT = ROOT / "missing_es_translations.ftl"


# ---------------------------------------------------------------------------
# FTL parsing
# ---------------------------------------------------------------------------

def parse_es_ftl_coverage(locale_dir: Path) -> tuple[set, set]:
    """
    Scan all .ftl files under locale_dir and return:
      has_name — entity IDs where ent-{id} = ... exists
      has_desc — entity IDs where ent-{id} has a .desc attribute
    """
    has_name: set[str] = set()
    has_desc: set[str] = set()

    for ftl_path in locale_dir.rglob("*.ftl"):
        try:
            content = ftl_path.read_text(encoding="utf-8")
        except Exception as e:
            print(f"Warning: could not read {ftl_path}: {e}", file=sys.stderr)
            continue

        current_id: str | None = None
        for line in content.splitlines():
            # New top-level ent-* message
            m = re.match(r'^(ent-(\S+?))\s*=', line)
            if m:
                current_id = m.group(2)
                has_name.add(current_id)
            # .desc attribute belonging to the current message
            elif current_id and re.match(r'^\s+\.desc\s*=', line):
                has_desc.add(current_id)
            # Any non-indented, non-comment line closes the current message
            elif line and not line[0].isspace() and not line.startswith('#'):
                current_id = None

    return has_name, has_desc


# ---------------------------------------------------------------------------
# YAML parsing
# ---------------------------------------------------------------------------

def _strip_quotes(value: str) -> str:
    if (value.startswith('"') and value.endswith('"')) or \
       (value.startswith("'") and value.endswith("'")):
        return value[1:-1]
    return value


def _strip_yaml_comment(value: str) -> str:
    """Strip inline YAML comments from plain scalar values (not quoted strings)."""
    if value.startswith('"') or value.startswith("'"):
        return value  # quoted — don't touch
    # In YAML, a space followed by # starts an inline comment
    return re.sub(r'\s+#.*$', '', value)


def _parse_parent_list(raw: str) -> list[str]:
    """Parse a YAML parent field which may be a string or inline list."""
    raw = raw.strip()
    if raw.startswith('['):
        # Inline list: [Id1, Id2, ...]
        inner = raw.lstrip('[').rstrip(']')
        return [p.strip() for p in inner.split(',') if p.strip()]
    return [raw] if raw else []


def parse_yaml_file(yaml_path: Path) -> list[dict]:
    """
    Parse all entity definitions from a YAML prototype file.
    Returns list of dicts with: id, name (may be None), description (may be None),
    parents (list of parent IDs), source (Path).
    """
    try:
        content = yaml_path.read_text(encoding="utf-8")
    except Exception as e:
        print(f"Warning: could not read {yaml_path}: {e}", file=sys.stderr)
        return []

    entities = []
    blocks = re.split(r'^- type:\s*entity\s*$', content, flags=re.MULTILINE)

    for block in blocks[1:]:
        # id — required
        id_m = re.search(r'^\s{2}id:\s*(.+?)\s*$', block, re.MULTILINE)
        if not id_m:
            continue
        entity_id = _strip_yaml_comment(id_m.group(1).strip())

        # Skip YAML alias references (start with *) — not real entity definitions
        if entity_id.startswith('*'):
            continue

        # parent(s) — optional, may be string or inline list
        parents: list[str] = []
        parent_m = re.search(r'^\s{2}parent:\s*(.+?)\s*$', block, re.MULTILINE)
        if parent_m:
            parents = _parse_parent_list(
                _strip_yaml_comment(parent_m.group(1).strip())
            )

        # name — at exactly 2-space indent (not deeper)
        name: str | None = None
        name_m = re.search(r'^\s{2}name:\s*(.+?)\s*$', block, re.MULTILINE)
        if name_m:
            raw = _strip_quotes(_strip_yaml_comment(name_m.group(1).strip()))
            if raw and not raw.startswith('{'):
                name = raw

        # description — optional, same indent level
        desc: str | None = None
        desc_m = re.search(r'^\s{2}description:\s*(.+?)\s*$', block, re.MULTILINE)
        if desc_m:
            raw = _strip_quotes(_strip_yaml_comment(desc_m.group(1).strip()))
            if raw:
                desc = raw

        entities.append({
            "id": entity_id,
            "name": name,
            "description": desc,
            "parents": parents,
            "source": yaml_path,
        })

    return entities


# ---------------------------------------------------------------------------
# Prototype inheritance resolution
# ---------------------------------------------------------------------------

def resolve_effective(entity_id: str, entity_map: dict, _visited: frozenset = frozenset()) -> tuple[str | None, str | None]:
    """
    Walk up the prototype parent chain to find the effective (inherited) name
    and description for an entity.
    Returns (name, description) — either may be None if not found.
    """
    if entity_id in _visited:
        return None, None  # cycle guard

    entity = entity_map.get(entity_id)
    if entity is None:
        return None, None

    own_name = entity.get("name")
    own_desc = entity.get("description")

    # If entity has both own name and own description, done.
    if own_name and own_desc:
        return own_name, own_desc

    # Walk parents to fill in what's missing
    visited = _visited | {entity_id}
    inherited_name: str | None = None
    inherited_desc: str | None = None

    for parent_id in entity.get("parents", []):
        p_name, p_desc = resolve_effective(parent_id, entity_map, visited)
        if p_name and not inherited_name:
            inherited_name = p_name
        if p_desc and not inherited_desc:
            inherited_desc = p_desc
        if inherited_name and inherited_desc:
            break

    return (own_name or inherited_name), (own_desc or inherited_desc)


# ---------------------------------------------------------------------------
# Output generation
# ---------------------------------------------------------------------------

def build_output(
    missing_both: dict[Path, list[dict]],
    missing_desc_only: dict[Path, list[dict]],
) -> str:
    """Build the full FTL output string."""
    lines: list[str] = [
        "## ================================================================",
        "## Missing es-ES Translations",
        "## Generated by: python Tools/find_missing_es_translations.py",
        "##",
        "## Translate the English values below, then merge into the correct",
        "## es-ES .ftl files under Resources/Locale/es-ES/",
        "## ================================================================",
    ]

    # --- Section 1: missing name (+ possibly desc) ---
    lines.append("")
    lines.append("## ----------------------------------------------------------------")
    lines.append("## SECTION 1 — MISSING NAME (+ description where present)")
    lines.append("## The ent-{id} key is completely absent from all es-ES locale files.")
    lines.append("## ----------------------------------------------------------------")

    if missing_both:
        for yaml_path in sorted(missing_both):
            entities = missing_both[yaml_path]
            rel = yaml_path.relative_to(ROOT).as_posix()
            lines.append("")
            lines.append(f"## Source: {rel}")
            for ent in entities:
                if ent.get("inherited_name"):
                    lines.append(f"## (name inherited from parent: {ent['inherited_from']})")
                lines.append(f"ent-{ent['id']} = {ent['eff_name']}")
                if ent["eff_desc"]:
                    lines.append(f"    .desc = {ent['eff_desc']}")
                lines.append("")
    else:
        lines.append("")
        lines.append("## (none — all entity names are translated)")

    # --- Section 2: missing desc only ---
    lines.append("")
    lines.append("## ----------------------------------------------------------------")
    lines.append("## SECTION 2 — MISSING DESCRIPTION ONLY")
    lines.append("## The name (ent-{id}) is already translated in es-ES, but the")
    lines.append("## .desc attribute is absent. Add it to the existing es-ES entry.")
    lines.append("## ----------------------------------------------------------------")

    if missing_desc_only:
        for yaml_path in sorted(missing_desc_only):
            entities = missing_desc_only[yaml_path]
            rel = yaml_path.relative_to(ROOT).as_posix()
            lines.append("")
            lines.append(f"## Source: {rel}")
            for ent in entities:
                if ent.get("inherited_desc"):
                    lines.append(f"## (desc inherited from parent: {ent['inherited_from']})")
                lines.append(f"## Add .desc to existing ent-{ent['id']} in es-ES:")
                lines.append(f"ent-{ent['id']} =")
                lines.append(f"    .desc = {ent['eff_desc']}")
                lines.append("")
    else:
        lines.append("")
        lines.append("## (none — all entity descriptions are translated)")

    return "\n".join(lines) + "\n"


# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------

def main() -> None:
    parser = argparse.ArgumentParser(
        description="Find missing es-ES entity translations (name and/or .desc)."
    )
    parser.add_argument(
        "--output", "-o",
        type=Path,
        default=DEFAULT_OUTPUT,
        help=f"Output file path (default: {DEFAULT_OUTPUT})",
    )
    args = parser.parse_args()

    print("=== Missing es-ES Translation Finder ===\n")

    # Step 1: scan es-ES FTL files
    print("Step 1: Scanning es-ES locale files...")
    has_name, has_desc = parse_es_ftl_coverage(LOCALE_ES_DIR)
    print(f"  Entity names translated:        {len(has_name)}")
    print(f"  Entity descriptions translated: {len(has_desc)}\n")

    # Step 2: parse ALL YAML prototypes into an entity map
    print("Step 2: Scanning YAML prototype files...")
    yaml_files = sorted(PROTOTYPES_DIR.rglob("*.yml"))
    print(f"  Found {len(yaml_files)} YAML files")

    # Build entity map: id -> entity dict (includes source file, parents, etc.)
    entity_map: dict[str, dict] = {}
    for yaml_path in yaml_files:
        for ent in parse_yaml_file(yaml_path):
            # Last definition wins if duplicates exist (shouldn't happen in valid SS14)
            entity_map[ent["id"]] = ent

    print(f"  Found {len(entity_map)} total entity definitions\n")

    # Step 3: resolve effective name/description and find gaps
    print("Step 3: Resolving inheritance and comparing against es-ES...")

    missing_both: dict[Path, list[dict]] = defaultdict(list)
    missing_desc_only: dict[Path, list[dict]] = defaultdict(list)
    total_with_name = 0
    already_complete = 0

    for entity_id, ent in entity_map.items():
        eff_name, eff_desc = resolve_effective(entity_id, entity_map)

        if not eff_name:
            continue  # truly nameless entity, skip

        total_with_name += 1
        source: Path = ent["source"]

        need_name = entity_id not in has_name
        need_desc = bool(eff_desc) and entity_id not in has_desc

        # Determine if name/desc are inherited (for output annotation)
        inherited_name = eff_name != ent.get("name")
        inherited_desc = eff_desc != ent.get("description")
        inherited_from = ent["parents"][0] if (inherited_name or inherited_desc) and ent["parents"] else None

        if need_name:
            missing_both[source].append({
                **ent,
                "eff_name": eff_name,
                "eff_desc": eff_desc,
                "inherited_name": inherited_name,
                "inherited_desc": inherited_desc,
                "inherited_from": inherited_from,
            })
        elif need_desc:
            missing_desc_only[source].append({
                **ent,
                "eff_name": eff_name,
                "eff_desc": eff_desc,
                "inherited_name": inherited_name,
                "inherited_desc": inherited_desc,
                "inherited_from": inherited_from,
            })
        else:
            already_complete += 1

    count_missing_name = sum(len(v) for v in missing_both.values())
    count_missing_desc = sum(len(v) for v in missing_desc_only.values())

    print(f"  Entities with a displayable name: {total_with_name}")
    print(f"  Already fully translated:         {already_complete}")
    print(f"  Missing name (+ maybe desc):      {count_missing_name}")
    print(f"  Missing desc only:                {count_missing_desc}\n")

    # Step 4: write output
    print(f"Step 4: Writing output to {args.output} ...")
    content = build_output(missing_both, missing_desc_only)
    args.output.write_text(content, encoding="utf-8")
    print(f"  Done. {count_missing_name + count_missing_desc} entries written.\n")

    print("=== Summary ===")
    print(f"  YAML files scanned:               {len(yaml_files)}")
    print(f"  Entities needing attention:       {count_missing_name + count_missing_desc}")
    print(f"    - Missing name (section 1):     {count_missing_name}")
    print(f"    - Missing desc only (sec. 2):   {count_missing_desc}")
    print(f"  Output:                           {args.output}")


if __name__ == "__main__":
    main()
