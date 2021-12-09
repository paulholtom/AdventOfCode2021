using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day1
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 1;

        /// <summary>
        /// The input, parsed from the original string to an array of ints.
        /// </summary>
        private int[] ParsedInput { get; set; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            ParsedInput = Input.ToIntArray();
        }

        /// <summary>
        /// Count the number of times there is in an increase in the input values.
        /// </summary>
        /// <param name="sampleSize">The space between values to check.</param>
        /// <returns>The number times there is an increase.</returns>
        public int GetIncreases(int sampleSize)
        {
            var count = 0;
            for (int i = sampleSize; i < ParsedInput.Length; i++)
            {
                if (ParsedInput[i - sampleSize] < ParsedInput[i]) ++count;
            }
            return count;
        }
        
        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The number of increases in adjacent values</returns>
        public int RunPart1()
        {
            return GetIncreases(1);
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The number of increases in values 3 steps apart.</returns>
        public int RunPart2()
        {
            return GetIncreases(3);
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
