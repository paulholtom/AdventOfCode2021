using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day3
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 3;

        /// <summary>
        /// The lines of input.
        /// </summary>
        protected string[] InputLines { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            InputLines = Input.SplitLines();
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            var numOn = new int[InputLines[0].Length];
            foreach (var line in InputLines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1') numOn[i]++;
                }
            }
            var gamma = 0;
            var epsilon = 0;

            for(int i = 0;i < numOn.Length; i++)
            {
                var amt = (int)Math.Pow(2, numOn.Length - i - 1);
                if (numOn[i] > InputLines.Length / 2)
                    gamma += amt;
                else
                    epsilon += amt;
            }
            return gamma * epsilon;
        }

        /// <summary>
        /// Filter the inputs based the criteria for oxygen/co2
        /// </summary>
        /// <param name="keepOn">How to determine if the "on" array should be kept for any given iteration.</param>
        /// <returns>The final number after applying the filtering.</returns>
        protected int Filter(Func<int, int, bool> keepOn)
        {
            List<string> on, off, remaining = new(InputLines);

            int pos = 0;
            while (remaining.Count > 1 && pos < remaining[0].Length)
            {
                on = new();
                off = new();
                foreach (var line in remaining)
                {
                    if (line[pos] == '1')
                    {
                        on.Add(line);
                    }
                    else
                    {
                        off.Add(line);
                    }
                }

                if (keepOn(on.Count, off.Count))
                {
                    remaining = on;
                }
                else
                {
                    remaining = off;
                }
                pos++;
            }
            return Convert.ToInt32(remaining[0], 2);
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            var oxygen = Filter((on, off) => on >= off);
            var co2 = Filter((on, off) => on < off);

            return oxygen * co2;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
