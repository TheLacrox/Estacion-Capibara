#!/usr/bin/env python3
"""
Extract missing entity localization keys from YAML prototypes.

Scans all YAML prototype files for entity definitions with name/description fields,
checks if corresponding ent-{id} keys exist in es-ES locale files, and generates
.ftl files in Resources/Locale/es-ES/_hispania/ for any missing entries.
"""

import os
import re
import sys
from pathlib import Path
from collections import defaultdict

# Project root
ROOT = Path(__file__).parent
PROTOTYPES_DIR = ROOT / "Resources" / "Prototypes"
LOCALE_ES_DIR = ROOT / "Resources" / "Locale" / "es-ES"
OUTPUT_DIR = LOCALE_ES_DIR / "_hispania"


def find_existing_ent_keys(locale_dir: Path) -> set:
    """Scan all .ftl files in es-ES for existing ent-* keys."""
    existing = set()
    pattern = re.compile(r'^(ent-\S+)\s*=', re.MULTILINE)

    for ftl_path in locale_dir.rglob("*.ftl"):
        try:
            content = ftl_path.read_text(encoding="utf-8")
            for match in pattern.finditer(content):
                existing.add(match.group(1))
        except Exception as e:
            print(f"Warning: Could not read {ftl_path}: {e}", file=sys.stderr)

    return existing


def parse_yaml_entities(yaml_path: Path) -> list:
    """
    Parse a YAML file for entity definitions with name/description.
    Uses regex-based parsing since entities follow a consistent format.
    Returns list of dicts with 'id', 'name', 'description' keys.
    """
    entities = []

    try:
        content = yaml_path.read_text(encoding="utf-8")
    except Exception as e:
        print(f"Warning: Could not read {yaml_path}: {e}", file=sys.stderr)
        return entities

    # Split into entity blocks: each starts with "- type: entity"
    # We use a regex to find entity blocks
    blocks = re.split(r'^- type:\s*entity\s*$', content, flags=re.MULTILINE)

    for block in blocks[1:]:  # Skip text before first entity
        entity = {}

        # Extract id
        id_match = re.search(r'^\s{2}id:\s*(.+?)\s*$', block, re.MULTILINE)
        if not id_match:
            continue
        entity['id'] = id_match.group(1).strip()

        # Extract name (must be at indent level 2, not deeper)
        name_match = re.search(r'^\s{2}name:\s*(.+?)\s*$', block, re.MULTILINE)
        if not name_match:
            continue  # Skip entities without explicit name
        entity['name'] = name_match.group(1).strip()

        # Remove quotes if present
        if entity['name'].startswith('"') and entity['name'].endswith('"'):
            entity['name'] = entity['name'][1:-1]
        if entity['name'].startswith("'") and entity['name'].endswith("'"):
            entity['name'] = entity['name'][1:-1]

        # Extract description (may be multiline)
        desc_match = re.search(r'^\s{2}description:\s*(.+?)\s*$', block, re.MULTILINE)
        if desc_match:
            desc = desc_match.group(1).strip()
            if desc.startswith('"') and desc.endswith('"'):
                desc = desc[1:-1]
            if desc.startswith("'") and desc.endswith("'"):
                desc = desc[1:-1]
            entity['description'] = desc
        else:
            entity['description'] = None

        # Skip entities with empty names or localization references
        if not entity['name'] or entity['name'].startswith('{'):
            continue

        entities.append(entity)

    return entities


def yaml_path_to_ftl_path(yaml_path: Path) -> Path:
    """
    Convert a YAML prototype path to a corresponding .ftl output path.
    e.g., Resources/Prototypes/Entities/Structures/Furniture/beds.yml
       -> Resources/Locale/es-ES/_hispania/prototypes/entities/structures/furniture/beds.ftl
    """
    # Get relative path from Prototypes dir
    try:
        rel = yaml_path.relative_to(PROTOTYPES_DIR)
    except ValueError:
        rel = Path(yaml_path.name)

    # Convert to lowercase path and change extension
    parts = [p.lower() for p in rel.parts]
    ftl_name = Path(*parts).with_suffix(".ftl")

    return OUTPUT_DIR / "prototypes" / ftl_name


def generate_ftl_content(entities: list) -> str:
    """Generate .ftl file content for a list of entities."""
    lines = []
    for i, ent in enumerate(entities):
        if i > 0:
            lines.append("")
        lines.append(f"ent-{ent['id']} = {ent['name']}")
        if ent.get('description'):
            # Escape any problematic characters in description
            desc = ent['description']
            lines.append(f"    .desc = {desc}")

    return "\n".join(lines) + "\n"


def main():
    print("=== Missing Entity Key Extractor ===\n")

    # Step 1: Find existing ent-* keys in es-ES
    print("Step 1: Scanning existing es-ES locale files for ent-* keys...")
    existing_keys = find_existing_ent_keys(LOCALE_ES_DIR)
    print(f"  Found {len(existing_keys)} existing ent-* keys\n")

    # Step 2: Parse all YAML prototypes
    print("Step 2: Scanning YAML prototype files...")
    yaml_files = list(PROTOTYPES_DIR.rglob("*.yml"))
    print(f"  Found {len(yaml_files)} YAML files")

    # Group entities by source file
    entities_by_file = defaultdict(list)
    total_entities = 0
    skipped_existing = 0

    for yaml_path in yaml_files:
        entities = parse_yaml_entities(yaml_path)
        for ent in entities:
            key = f"ent-{ent['id']}"
            if key in existing_keys:
                skipped_existing += 1
                continue
            entities_by_file[yaml_path].append(ent)
            total_entities += 1

    print(f"  Found {total_entities} entities with missing translations")
    print(f"  Skipped {skipped_existing} entities (already translated)\n")

    # Step 3: Generate .ftl files
    print("Step 3: Generating .ftl files in _hispania/...")
    files_created = 0
    total_lines = 0

    for yaml_path, entities in sorted(entities_by_file.items()):
        if not entities:
            continue

        ftl_path = yaml_path_to_ftl_path(yaml_path)
        ftl_path.parent.mkdir(parents=True, exist_ok=True)

        content = generate_ftl_content(entities)
        ftl_path.write_text(content, encoding="utf-8")
        files_created += 1
        total_lines += content.count("\n")

    print(f"  Created {files_created} .ftl files")
    print(f"  Total lines: {total_lines}")
    print(f"  Output directory: {OUTPUT_DIR}")

    # Summary
    print(f"\n=== Summary ===")
    print(f"  YAML files scanned: {len(yaml_files)}")
    print(f"  Entities needing translation: {total_entities}")
    print(f"  Entities already translated: {skipped_existing}")
    print(f"  FTL files generated: {files_created}")
    print(f"  Total translation lines: {total_lines}")


if __name__ == "__main__":
    main()
