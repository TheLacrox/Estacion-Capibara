#!/usr/bin/env python3
"""
AI-assisted bulk translation of .ftl locale files from en-US to es-ES.

Requires an Anthropic API key set as ANTHROPIC_API_KEY environment variable.
Install dependency: pip install anthropic

Usage:
    # Translate a single file
    python Tools/translate_ftl.py Resources/Locale/en-US/actions/actions/combat-mode.ftl

    # Translate a directory (all .ftl files recursively)
    python Tools/translate_ftl.py Resources/Locale/en-US/lobby/

    # Translate everything (will take a long time and many API calls)
    python Tools/translate_ftl.py Resources/Locale/en-US/

    # Dry run — show what would be translated without calling the API
    python Tools/translate_ftl.py --dry-run Resources/Locale/en-US/lobby/

    # Skip files that have already been translated (differ from en-US)
    python Tools/translate_ftl.py --skip-translated Resources/Locale/en-US/lobby/
"""

import argparse
import filecmp
import os
import re
import sys
import time

LOCALE_DIR = os.path.join(os.path.dirname(__file__), "..", "Resources", "Locale")
SOURCE_LOCALE = "en-US"
TARGET_LOCALE = "es-ES"

SYSTEM_PROMPT = """\
You are a professional translator for a Space Station 14 game server (a multiplayer space station simulator). \
Translate the following Project Fluent (.ftl) localization file from English to Spanish (Spain/es-ES variant).

Rules:
1. Translate ONLY the values (right side of "="). NEVER change the message keys (left side of "=").
2. Preserve ALL Fluent syntax exactly: variables like {$name}, {$count}, selectors (.one, .other, etc.), \
attributes (lines starting with .), references to other messages like {message-id}, and terms like {-term-id}.
3. Preserve ALL comment lines (lines starting with #) exactly as-is — do not translate comments.
4. Preserve blank lines and file structure exactly.
5. Use natural, idiomatic Spanish for Spain (es-ES). Use gaming terminology appropriate for a space station game.
6. For Fluent selectors with plural forms, use correct Spanish plural rules \
(Spanish has "one" and "other" categories, same as English).
7. Do NOT add or remove any lines. The output must have the same number of lines as the input.
8. Return ONLY the translated file content, no explanations or markdown code blocks.
"""

# How many .ftl files to batch into a single API call
BATCH_SIZE = 5

# Rate limiting: seconds to wait between API calls
RATE_LIMIT_DELAY = 1.0


def get_source_files(path: str) -> list[str]:
    """Get list of .ftl files from a path (file or directory)."""
    path = os.path.normpath(path)
    if os.path.isfile(path) and path.endswith(".ftl"):
        return [path]
    if os.path.isdir(path):
        result = []
        for root, _dirs, files in os.walk(path):
            for f in sorted(files):
                if f.endswith(".ftl"):
                    result.append(os.path.join(root, f))
        return result
    print(f"ERROR: {path} is not a .ftl file or directory")
    sys.exit(1)


def source_to_target(source_path: str) -> str:
    """Convert en-US file path to es-ES file path."""
    return source_path.replace(
        os.sep + SOURCE_LOCALE + os.sep,
        os.sep + TARGET_LOCALE + os.sep,
    ).replace(
        "/" + SOURCE_LOCALE + "/",
        "/" + TARGET_LOCALE + "/",
    )


def is_already_translated(source_path: str, target_path: str) -> bool:
    """Check if a target file exists and differs from the source (i.e., was already translated)."""
    if not os.path.isfile(target_path):
        return False
    return not filecmp.cmp(source_path, target_path, shallow=False)


def translate_file_content(client, content: str, rel_path: str) -> str:
    """Send a single .ftl file to the AI for translation."""
    message = client.messages.create(
        model="claude-sonnet-4-20250514",
        max_tokens=8192,
        system=SYSTEM_PROMPT,
        messages=[
            {
                "role": "user",
                "content": f"Translate this file ({rel_path}):\n\n{content}",
            }
        ],
    )
    return message.content[0].text


def main():
    parser = argparse.ArgumentParser(description="AI-assisted .ftl translation (en-US -> es-ES)")
    parser.add_argument("path", help="Path to a .ftl file or directory of .ftl files under en-US/")
    parser.add_argument("--dry-run", action="store_true", help="Show files that would be translated without calling API")
    parser.add_argument("--skip-translated", action="store_true", help="Skip files where es-ES already differs from en-US")
    args = parser.parse_args()

    source_files = get_source_files(args.path)
    if not source_files:
        print("No .ftl files found.")
        return

    # Filter to only files under en-US
    source_files = [f for f in source_files if SOURCE_LOCALE in f]
    if not source_files:
        print(f"No files under {SOURCE_LOCALE}/ found. Provide a path under Resources/Locale/{SOURCE_LOCALE}/")
        return

    # Build work list
    work = []
    for source_path in source_files:
        target_path = source_to_target(source_path)
        rel_path = os.path.relpath(source_path, os.path.join(LOCALE_DIR, SOURCE_LOCALE))

        if args.skip_translated and is_already_translated(source_path, target_path):
            continue

        work.append((source_path, target_path, rel_path))

    print(f"Files to translate: {len(work)}")
    if args.dry_run:
        for _, _, rel_path in work:
            print(f"  {rel_path}")
        return

    if not work:
        print("Nothing to translate.")
        return

    # Import anthropic only when actually translating
    try:
        import anthropic
    except ImportError:
        print("ERROR: Install the anthropic package: pip install anthropic")
        sys.exit(1)

    api_key = os.environ.get("ANTHROPIC_API_KEY")
    if not api_key:
        print("ERROR: Set ANTHROPIC_API_KEY environment variable")
        sys.exit(1)

    client = anthropic.Anthropic(api_key=api_key)

    translated = 0
    errors = 0

    for source_path, target_path, rel_path in work:
        print(f"[{translated + errors + 1}/{len(work)}] Translating: {rel_path} ... ", end="", flush=True)

        try:
            with open(source_path, "r", encoding="utf-8") as f:
                content = f.read()

            # Skip near-empty files (only comments/blank lines, no actual keys)
            if not re.search(r"^[\w-]+\s*=", content, re.MULTILINE):
                print("SKIP (no translatable keys)")
                # Still copy the file as-is (comments only)
                os.makedirs(os.path.dirname(target_path), exist_ok=True)
                with open(target_path, "w", encoding="utf-8") as f:
                    f.write(content)
                translated += 1
                continue

            result = translate_file_content(client, content, rel_path)

            os.makedirs(os.path.dirname(target_path), exist_ok=True)
            with open(target_path, "w", encoding="utf-8") as f:
                f.write(result)
                if not result.endswith("\n"):
                    f.write("\n")

            print("OK")
            translated += 1

        except Exception as e:
            print(f"ERROR: {e}")
            errors += 1

        time.sleep(RATE_LIMIT_DELAY)

    print()
    print(f"Done. Translated: {translated}, Errors: {errors}")


if __name__ == "__main__":
    main()
