using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal static class InputParsing
    {
        /// <summary>
        /// Split the input string into an array on line returns.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>An array of the strings, each of which is a line in the input.</returns>
        internal static string[] SplitLines(this string input)
        {
            return input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }

        /// <summary>
        /// Convert a string with number separated by line returns into an array of integers.
        /// </summary>
        /// <param name="input">The string</param>
        /// <returns>The array of integers.</returns>
        internal static int[] ToIntArray(this string input)
        {
            List<int> numbers = new List<int>();

            foreach (var number in input.SplitLines())
            {
                numbers.Add(int.Parse(number));
            }

            return numbers.ToArray();
        }
    }
}
