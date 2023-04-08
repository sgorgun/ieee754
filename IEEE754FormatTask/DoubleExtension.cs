using System;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;

namespace IEEE754FormatTask
{
    [StructLayout(LayoutKind.Explicit)]
    public struct DoubleExtension
    {
        [FieldOffset(0)]
        public double DoubleTerm;

        [FieldOffset(0)]
        public long LongTerm;
    }

    public static class DoubleExtensionClass
    {
        /// <summary>
        /// Returns a string representation of a double type number
        /// in the IEEE754 (see https://en.wikipedia.org/wiki/IEEE_754) format.
        /// </summary>
        /// <param name="number">Input number.</param>
        /// <returns>A string representation of a double type number in the IEEE754 format.</returns>
        public static string GetIEEE754Format(this double number)
        {
            DoubleExtension converter = new DoubleExtension() { DoubleTerm = number };
            long longNumber = converter.LongTerm;
            const int bitsInByte = 8;
            const int bitsCount = sizeof(double) * bitsInByte;
            char[] result = new char[bitsCount];
            result[0] = longNumber < 0 ? '1' : '0';
            for (int i = bitsCount - 2, j = 1; i >= 0; i--, j++)
            {
                result[j] = (longNumber & (1L << i)) != 0 ? '1' : '0';
            }

            return new string(result);
        }
    }
}
