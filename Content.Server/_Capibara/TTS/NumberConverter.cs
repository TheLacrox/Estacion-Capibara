// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.RegularExpressions;

namespace Content.Server._Capibara.TTS;

/// <summary>
/// Converts numbers in text to their Spanish word representations for more natural TTS output.
/// The server is Spanish-first, so numbers are spelled out in Spanish (e.g. 123 -> "ciento veintitrés").
/// </summary>
public static partial class NumberConverter
{
    // 0-19 (irregular forms).
    private static readonly string[] Ones =
    {
        "cero", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve",
        "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis",
        "diecisiete", "dieciocho", "diecinueve"
    };

    // 20-29 are written as a single word in Spanish.
    private static readonly string[] Twenties =
    {
        "veinte", "veintiuno", "veintidós", "veintitrés", "veinticuatro", "veinticinco",
        "veintiséis", "veintisiete", "veintiocho", "veintinueve"
    };

    // Tens, indexed by number / 10. 30+ join the ones with " y " (e.g. "treinta y uno").
    private static readonly string[] Tens =
    {
        "", "", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"
    };

    // Hundreds, indexed by number / 100. Index 1 ("ciento") is only used for 101-199;
    // exactly 100 is the apocopated "cien" (handled in NumberToWords).
    private static readonly string[] Hundreds =
    {
        "", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos",
        "seiscientos", "setecientos", "ochocientos", "novecientos"
    };

    /// <summary>
    /// Replace numeric sequences in text with Spanish word equivalents.
    /// Only converts numbers up to 999 to keep output reasonable.
    /// </summary>
    public static string ConvertNumbersToWords(string text)
    {
        return NumberRegex().Replace(text, match =>
        {
            if (!int.TryParse(match.Value, out var number))
                return match.Value;

            if (number is < 0 or > 999)
                return match.Value;

            return NumberToWords(number);
        });
    }

    private static string NumberToWords(int number)
    {
        if (number < 20)
            return Ones[number];

        if (number < 30)
            return Twenties[number - 20];

        if (number < 100)
        {
            var tens = Tens[number / 10];
            var remainder = number % 10;
            return remainder == 0 ? tens : tens + " y " + Ones[remainder];
        }

        // 100-999
        if (number == 100)
            return "cien";

        var hundreds = Hundreds[number / 100];
        var rest = number % 100;
        return rest == 0 ? hundreds : hundreds + " " + NumberToWords(rest);
    }

    [GeneratedRegex(@"\b\d+\b")]
    private static partial Regex NumberRegex();
}
