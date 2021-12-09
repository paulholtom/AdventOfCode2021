using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    /// <summary>
    /// Base class for each day's solution.
    /// </summary>
    public abstract class SolutionBase
    {
        /// <summary>
        /// The number of the day.
        /// </summary>
        protected abstract int Day { get; }

        /// <summary>
        /// The day's input.
        /// </summary>
        protected string Input { get; }

        /// <summary>
        /// Basic constructor. Assigns the input to a class member.
        /// </summary>
        /// <param name="input">The input. If not provided reads from the file instead.</param>
        protected SolutionBase(string? input = null)
        {
            Input = input ?? input ?? File.ReadAllText($"Days/Day{Day}/input.txt");
        }

        /// <summary>
        /// Builds an output string for the solution.
        /// </summary>
        /// <param name="part1">The solution for part 1</param>
        /// <param name="part2">The solution for part 2</param>
        /// <returns>The string to be output.</returns>
        protected string GetOuputString(string part1, string part2)
        {
            return $"Day{Day}\r\n\r\nPart 1:\r\n{part1}\r\n\r\nPart 2:\r\n{part2}";
        }

        /// <summary>
        /// Generate a string that shows the solutions for both parts.
        /// </summary>
        /// <returns>String with the solutions.</returns>
        public abstract string Run();
    }
}
