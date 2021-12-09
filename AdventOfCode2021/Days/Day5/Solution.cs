using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2021.Coordinates;

namespace AdventOfCode2021.Days.Day5
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 5;

        /// <summary>
        /// The input split by lines.
        /// </summary>
        protected string[] InputLines { get; }

        /// <summary>
        /// The lines;
        /// </summary>
        protected List<Line2d> Lines { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            InputLines = Input.SplitLines();
            Lines = new List<Line2d>();

            foreach(var line in InputLines)
            {
                Lines.Add(new Line2d(line));
            }
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            Grid2d grid = new();
            
            foreach(var line in Lines)
            {
                if(line.IsHorizontal || line.IsVertical)
                {
                    grid.DrawLine(line);
                }
            }

            return grid.CountOverlaps();
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            Grid2d grid = new();

            foreach (var line in Lines)
            {
                grid.DrawLine(line);
            }

            return grid.CountOverlaps();
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
