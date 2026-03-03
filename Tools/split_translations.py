#!/usr/bin/env python3
"""
Split missing_es_translations.ftl into small batch JSON files for translation.

Usage:
    python Tools/split_translations.py               # parse + create batches
    python Tools/split_translations.py --batch-size 30
    python Tools/split_translations.py --status      # show done/pending counts
    python Tools/split_translations.py --reset       # delete all batches and restart
"""

import argparse
import json
import re
import sys
from pathlib import Path

ROOT = Path(__file__).parent.parent
MISSING_FILE = ROOT / "missing_es_translations.ftl"
BATCHES_DIR = ROOT / "Tools" / "translation_batches"
LOCALE_ES = ROOT / "Resources" / "Locale" / "es-ES"
PROTOTYPES = ROOT / "Resources" / "Prototypes"

DEFAULT_BATCH_SIZE = 50


# ---------------------------------------------------------------------------
# FTL entity-to-file map (for Section 2: where does ent-{id} already live?)
# ---------------------------------------------------------------------------

def build_entity_ftl_map(locale_dir: Path) -> dict[str, Path]:
    """Scan all es-ES FTL files and return {entity_id: ftl_path}."""
    entity_map: dict[str, Path] = {}
    for ftl_path in locale_dir.rglob("*.ftl"):
        try:
            content = ftl_path.read_text(encoding="utf-8")
        except Exception as e:
            print(f"  Warning: could not read {ftl_path}: {e}", file=sys.stderr)
            continue
        for m in re.finditer(r'^ent-(\S+?)\s*=', content, re.MULTILINE):
            entity_map[m.group(1)] = ftl_path
    return entity_map


# ---------------------------------------------------------------------------
# Parsing missing_es_translations.ftl
# ---------------------------------------------------------------------------

def split_sections(text: str) -> tuple[str, str]:
    """Return (section1_text, section2_text)."""
    marker = "## SECTION 2"
    idx = text.find(marker)
    if idx == -1:
        return text, ""
    return text[:idx], text[idx:]


def parse_source_groups(section_text: str) -> list[tuple[str, list[str]]]:
    """
    Split a section by '## Source:' headers.
    Returns list of (source_yaml_rel_path, [raw_ent_blocks]).
    """
    # Split on lines that start with "## Source:"
    parts = re.split(r'^## Source:\s*', section_text, flags=re.MULTILINE)
    groups = []
    for part in parts[1:]:
        lines = part.splitlines(keepends=True)
        if not lines:
            continue
        # First line is the yaml path
        source_yaml = lines[0].strip()
        rest = "".join(lines[1:])

        # Collect ent- blocks: split on blank lines, keep blocks that contain
        # an ent- line (blocks may be prefixed with ## comment lines).
        # raw_en starts at the first ent- line (strips leading comments).
        raw_blocks = []
        current: list[str] = []

        def flush_block() -> None:
            if not current:
                return
            block_lines = current[:]
            # Find first line starting with ent-
            ent_start = next((i for i, l in enumerate(block_lines) if l.startswith("ent-")), None)
            if ent_start is not None:
                raw_en = "\n".join(block_lines[ent_start:]).strip()
                if raw_en:
                    raw_blocks.append(raw_en)

        for line in rest.splitlines():
            if line.strip() == "":
                flush_block()
                current = []
            else:
                current.append(line)
        flush_block()  # flush last block

        if raw_blocks:
            groups.append((source_yaml, raw_blocks))

    return groups


def yaml_to_ftl_path(source_yaml_rel: str) -> Path:
    """
    Map a YAML prototype relative path to the corresponding es-ES FTL path.
    e.g. 'Resources/Prototypes/_Capibara/Economy/entities.yml'
      →  ROOT / 'Resources/Locale/es-ES/_Capibara/Economy/entities.ftl'
    """
    # Strip leading 'Resources/Prototypes/' prefix
    prefix = "Resources/Prototypes/"
    if source_yaml_rel.startswith(prefix):
        rel = source_yaml_rel[len(prefix):]
    else:
        rel = source_yaml_rel
    # Replace .yml → .ftl
    rel = re.sub(r'\.yml$', '.ftl', rel)
    return ROOT / "Resources" / "Locale" / "es-ES" / rel


def extract_entity_id(raw_block: str) -> str | None:
    m = re.match(r'^ent-(\S+?)\s*=', raw_block)
    return m.group(1) if m else None


# ---------------------------------------------------------------------------
# Create batch JSON files
# ---------------------------------------------------------------------------

def create_batches(batch_size: int, entity_ftl_map: dict[str, Path]) -> int:
    """Parse missing_es_translations.ftl and write batch JSON files. Returns batch count."""
    text = MISSING_FILE.read_text(encoding="utf-8")
    s1_text, s2_text = split_sections(text)

    s1_groups = parse_source_groups(s1_text)
    s2_groups = parse_source_groups(s2_text)

    total_entries_s1 = sum(len(g[1]) for g in s1_groups)
    total_entries_s2 = sum(len(g[1]) for g in s2_groups)
    print(f"  Section 1 entries: {total_entries_s1}")
    print(f"  Section 2 entries: {total_entries_s2}")

    BATCHES_DIR.mkdir(parents=True, exist_ok=True)

    batch_num = 0

    # --- Section 1 batches ---
    current_batch: list[dict] = []

    def flush_batch(section: int) -> None:
        nonlocal batch_num, current_batch
        if not current_batch:
            return
        batch_num += 1
        batch_id = f"batch_{batch_num:04d}_s{section}"
        out = {
            "batch_id": batch_id,
            "section": section,
            "entries": current_batch,
        }
        (BATCHES_DIR / f"{batch_id}.json").write_text(
            json.dumps(out, ensure_ascii=False, indent=2), encoding="utf-8"
        )
        current_batch = []

    for source_yaml, blocks in s1_groups:
        for block in blocks:
            entity_id = extract_entity_id(block)
            if not entity_id:
                continue
            target_ftl = yaml_to_ftl_path(source_yaml)
            entry = {
                "id": entity_id,
                "raw_en": block,
                "source_yaml": source_yaml,
                "target_ftl": str(target_ftl.relative_to(ROOT).as_posix()),
                "file_exists": target_ftl.exists(),
            }
            current_batch.append(entry)
            if len(current_batch) >= batch_size:
                flush_batch(1)
    flush_batch(1)

    # --- Section 2 batches ---
    for source_yaml, blocks in s2_groups:
        for block in blocks:
            entity_id = extract_entity_id(block)
            if not entity_id:
                continue
            existing_ftl = entity_ftl_map.get(entity_id)
            entry = {
                "id": entity_id,
                "raw_en": block,
                "source_yaml": source_yaml,
                "existing_ftl": str(existing_ftl.relative_to(ROOT).as_posix()) if existing_ftl else None,
            }
            current_batch.append(entry)
            if len(current_batch) >= batch_size:
                flush_batch(2)
    flush_batch(2)

    return batch_num


# ---------------------------------------------------------------------------
# Status
# ---------------------------------------------------------------------------

def print_status() -> None:
    if not BATCHES_DIR.exists():
        print("No batches directory found. Run without --status to create batches.")
        return

    all_json = sorted(BATCHES_DIR.glob("*.json"))
    done = [f for f in all_json if (BATCHES_DIR / f.stem).with_suffix(".done").exists()]
    error = [f for f in all_json if (BATCHES_DIR / f.stem).with_suffix(".error").exists()]
    pending = [f for f in all_json
               if not (BATCHES_DIR / f.stem).with_suffix(".done").exists()
               and not (BATCHES_DIR / f.stem).with_suffix(".error").exists()]

    s1_total = len([f for f in all_json if "_s1" in f.stem])
    s2_total = len([f for f in all_json if "_s2" in f.stem])
    s1_done = len([f for f in done if "_s1" in f.stem])
    s2_done = len([f for f in done if "_s2" in f.stem])

    print(f"Total batches:   {len(all_json)}")
    print(f"  Section 1:     {s1_total}  ({s1_done} done)")
    print(f"  Section 2:     {s2_total}  ({s2_done} done)")
    print(f"Done:            {len(done)}")
    print(f"Errors:          {len(error)}")
    print(f"Pending:         {len(pending)}")

    if pending:
        print("\nPending batches:")
        for f in pending[:10]:
            print(f"  {f.stem}")
        if len(pending) > 10:
            print(f"  ... and {len(pending) - 10} more")


# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------

def main() -> None:
    parser = argparse.ArgumentParser(
        description="Split missing_es_translations.ftl into batch JSON files for translation."
    )
    parser.add_argument("--batch-size", type=int, default=DEFAULT_BATCH_SIZE,
                        help=f"Entries per batch (default: {DEFAULT_BATCH_SIZE})")
    parser.add_argument("--status", action="store_true",
                        help="Show progress status and exit")
    parser.add_argument("--reset", action="store_true",
                        help="Delete all batch files and recreate from scratch")
    args = parser.parse_args()

    if args.status:
        print_status()
        return

    if args.reset and BATCHES_DIR.exists():
        import shutil
        shutil.rmtree(BATCHES_DIR)
        print(f"Cleared {BATCHES_DIR}")

    print("=== Translation Batch Splitter ===\n")

    if not MISSING_FILE.exists():
        print(f"Error: {MISSING_FILE} not found.", file=sys.stderr)
        print("Run: python Tools/find_missing_es_translations.py", file=sys.stderr)
        sys.exit(1)

    print("Step 1: Building entity->FTL map from es-ES locale files...")
    entity_ftl_map = build_entity_ftl_map(LOCALE_ES)
    print(f"  Found {len(entity_ftl_map)} translated entity IDs\n")

    print(f"Step 2: Parsing {MISSING_FILE.name} and creating batches (size={args.batch_size})...")
    batch_count = create_batches(args.batch_size, entity_ftl_map)

    print(f"\nDone! Created {batch_count} batch files in {BATCHES_DIR}/")
    print("\nNext: run Claude Code to process the batches.")
    print("      Use --status to check progress at any time.")


if __name__ == "__main__":
    main()
