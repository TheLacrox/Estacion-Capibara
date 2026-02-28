#!/usr/bin/env python3
"""
Compare en-US and es-ES locale files to find missing translations.

Usage:
    python Tools/check_translations.py                    # Full report
    python Tools/check_translations.py --summary          # Summary only
    python Tools/check_translations.py --dir _Goobstation # Check specific subdirectory
"""

import argparse
import os
import re
import sys

LOCALE_DIR = os.path.join(os.path.dirname(__file__), "..", "Resources", "Locale")
SOURCE_LOCALE = "en-US"
TARGET_LOCALE = "es-ES"

# Matches Fluent message/term IDs (lines like "message-id =" or "-term-id =")
KEY_PATTERN = re.compile(r"^(-?[\w][\w-]*)\s*=", re.MULTILINE)


def extract_keys(filepath: str) -> set[str]:
    """Extract all Fluent message/term keys from a .ftl file."""
    try:
        with open(filepath, "r", encoding="utf-8") as f:
            content = f.read()
    except (OSError, UnicodeDecodeError):
        return set()
    return set(KEY_PATTERN.findall(content))


def check_translations(subdir: str | None = None, summary_only: bool = False) -> tuple[int, int, int]:
    """
    Compare source and target locale directories.
    Returns (missing_files, missing_keys, total_source_keys).
    """
    source_dir = os.path.join(LOCALE_DIR, SOURCE_LOCALE)
    target_dir = os.path.join(LOCALE_DIR, TARGET_LOCALE)

    if subdir:
        source_dir = os.path.join(source_dir, subdir)
        target_dir = os.path.join(target_dir, subdir)

    if not os.path.isdir(source_dir):
        print(f"ERROR: Source directory not found: {source_dir}")
        return 0, 0, 0

    missing_files = 0
    missing_keys_total = 0
    total_source_keys = 0

    for root, _dirs, files in os.walk(source_dir):
        for filename in sorted(files):
            if not filename.endswith(".ftl"):
                continue

            source_path = os.path.join(root, filename)
            rel_path = os.path.relpath(source_path, os.path.join(LOCALE_DIR, SOURCE_LOCALE))
            target_path = os.path.join(LOCALE_DIR, TARGET_LOCALE, rel_path)

            source_keys = extract_keys(source_path)
            total_source_keys += len(source_keys)

            if not os.path.isfile(target_path):
                missing_files += 1
                missing_keys_total += len(source_keys)
                if not summary_only:
                    print(f"MISSING FILE: {TARGET_LOCALE}/{rel_path} ({len(source_keys)} keys)")
                continue

            target_keys = extract_keys(target_path)
            missing = source_keys - target_keys
            if missing:
                missing_keys_total += len(missing)
                if not summary_only:
                    for key in sorted(missing):
                        print(f"MISSING KEY:  {TARGET_LOCALE}/{rel_path}: {key}")

    return missing_files, missing_keys_total, total_source_keys


def main():
    parser = argparse.ArgumentParser(description="Check for missing translations between en-US and es-ES")
    parser.add_argument("--summary", action="store_true", help="Only print summary counts")
    parser.add_argument("--dir", type=str, default=None, help="Check a specific subdirectory (e.g., _Goobstation)")
    args = parser.parse_args()

    missing_files, missing_keys, total_keys = check_translations(
        subdir=args.dir,
        summary_only=args.summary,
    )

    print()
    print("=" * 60)
    print(f"Translation check: {SOURCE_LOCALE} -> {TARGET_LOCALE}")
    if args.dir:
        print(f"Subdirectory: {args.dir}")
    print(f"Total source keys:    {total_keys}")
    print(f"Missing files:        {missing_files}")
    print(f"Missing keys:         {missing_keys}")
    if total_keys > 0:
        coverage = ((total_keys - missing_keys) / total_keys) * 100
        print(f"Translation coverage: {coverage:.1f}%")
    print("=" * 60)

    if missing_keys > 0:
        sys.exit(1)


if __name__ == "__main__":
    main()
