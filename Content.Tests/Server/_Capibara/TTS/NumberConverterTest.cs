// SPDX-FileCopyrightText: 2025 Capibara Station Contributors
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server._Capibara.TTS;
using NUnit.Framework;

namespace Content.Tests.Server._Capibara.TTS
{
    [TestFixture]
    public sealed class NumberConverterTest
    {
        // Spanish-first server: numbers must be spelled out in Spanish, not English.
        [Test]
        [TestCase("0", "cero")]
        [TestCase("1", "uno")]
        [TestCase("15", "quince")]
        [TestCase("16", "dieciséis")]
        [TestCase("20", "veinte")]
        [TestCase("21", "veintiuno")]
        [TestCase("23", "veintitrés")]
        [TestCase("30", "treinta")]
        [TestCase("31", "treinta y uno")]
        [TestCase("45", "cuarenta y cinco")]
        [TestCase("99", "noventa y nueve")]
        [TestCase("100", "cien")]
        [TestCase("101", "ciento uno")]
        [TestCase("123", "ciento veintitrés")]
        [TestCase("200", "doscientos")]
        [TestCase("215", "doscientos quince")]
        [TestCase("500", "quinientos")]
        [TestCase("999", "novecientos noventa y nueve")]
        public void ConvertsToSpanish(string input, string expected)
        {
            Assert.That(NumberConverter.ConvertNumbersToWords(input), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertsNumbersEmbeddedInText()
        {
            Assert.That(
                NumberConverter.ConvertNumbersToWords("Quedan 3 minutos y 21 segundos"),
                Is.EqualTo("Quedan tres minutos y veintiuno segundos"));
        }

        // Out-of-range numbers are left as digits (the voice reads them in its own locale).
        [Test]
        [TestCase("1000", "1000")]
        [TestCase("2024", "2024")]
        public void LeavesOutOfRangeNumbersUnchanged(string input, string expected)
        {
            Assert.That(NumberConverter.ConvertNumbersToWords(input), Is.EqualTo(expected));
        }
    }
}
