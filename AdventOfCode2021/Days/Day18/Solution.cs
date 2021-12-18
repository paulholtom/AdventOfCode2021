using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day18
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 18;

        /// <summary>
        /// The list of parsed snailfish numbers.
        /// </summary>
        protected List<SnailfishPair> SnailfishNumbers { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var lines = Input.SplitLines();
            SnailfishNumbers = new();

            foreach (var line in lines)
            {
                var (num, _) = ParseNumberPart(line);
                SnailfishNumbers.Add((SnailfishPair)num);
            }
        }

        /// <summary>
        /// Parse snailfish numbers.
        /// </summary>
        /// <param name="numberString">The string to parse.</param>
        /// <returns>The first number part in the string and the number of characters it was.</returns>
        protected (SnailfishNumberPart, int) ParseNumberPart(string numberString)
        {
            if (numberString[0] != '[')
                return (new SnailfishRegular(numberString[0] - '0'), 1);

            var (left, lengthLeft) = ParseNumberPart(numberString.Substring(1));

            // Start this string at length + 1(opening bracket) + 1(comma)
            var (right, lengthRight) = ParseNumberPart(numberString.Substring(2 + lengthLeft));

            // Add 3 for the opening, closing brace and the comma.
            return (new SnailfishPair(left, right), 3 + lengthLeft + lengthRight);
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            var sum = SnailfishNumbers[0];
            for(int i = 1; i < SnailfishNumbers.Count; ++i)
            {
                sum += SnailfishNumbers[i];
            }
            return sum.GetMagnitude();
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            var largest = 0;
            for(int i = 0; i < SnailfishNumbers.Count; i++)
            {
                for(int j = 0; j < SnailfishNumbers.Count; j++)
                {
                    if(i != j)
                    {
                        var magnitude = (SnailfishNumbers[i] + SnailfishNumbers[j]).GetMagnitude();
                        if(magnitude > largest) 
                            largest = magnitude;
                    }
                }
            }
            return largest;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
