using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day2
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 2;

        /// <summary>
        /// The input split into lines.
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
            var sub = new SubmarineNoAim();
            sub.RunInstructions(InputLines);

            return sub.Depth * sub.HorizontalPosition;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            var sub = new Submarine();
            sub.RunInstructions(InputLines);

            return sub.Depth * sub.HorizontalPosition;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
