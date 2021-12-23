using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day23
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 23;

        /// <summary>
        /// A cache of the cheapest paths from various states.
        /// </summary>
        public Dictionary<string, int?> CachedCheapest { get; }

        /// <summary>
        /// The input brokenm up into lines.
        /// </summary>
        public string[] InputLines { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            CachedCheapest = new();
            InputLines = Input.SplitLines();
        }

        /// <summary>
        /// Get the cost of the cheapest path from this state to the completed state.
        /// </summary>
        /// <param name="state">The state to check from.</param>
        /// <returns>The value of the cheapest path. Null if there is no path to completion.</returns>
        public int? GetCheapestFromState(State state)
        {
            var key = state.ToString();
            if (CachedCheapest.ContainsKey(key)) return CachedCheapest[key];
            if (state.IsComplete()) return 0;
            int? min = null;
            var moves = state.GetValidMoves();
            foreach(var move in moves)
            {
                var cost = move.Cost + GetCheapestFromState(move.State);
                if (cost != null && (min == null || cost < min))
                {
                    min = cost.Value;
                }
            }
            CachedCheapest[key] = min;
            return min;
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            var state = new State(InputLines, 2);
            var val = GetCheapestFromState(state);
            if (val != null) return val.Value;
            return int.MaxValue;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            var lines = new string[InputLines.Length + 2];
            lines[0] = InputLines[0];
            lines[1] = InputLines[1];
            lines[2] = InputLines[2];
            
            lines[3] = "  #D#C#B#A#";
            lines[4] = "  #D#B#A#C#";

            lines[5] = InputLines[3];
            lines[6] = InputLines[4];

            var state = new State(lines, 4);

            var val = GetCheapestFromState(state);
            if (val != null) return val.Value;
            return int.MaxValue;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
