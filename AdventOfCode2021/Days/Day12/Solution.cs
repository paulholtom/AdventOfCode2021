using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day12
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 12;

        /// <summary>
        /// The start cave.
        /// </summary>
        const string START_CAVE = "start";
        /// <summary>
        /// The end cave.
        /// </summary>
        const string END_CAVE = "end";

        /// <summary>
        /// All possible connections from one cave to another.
        /// </summary>
        Dictionary<string, List<string>> Connections { get; }

        /// <summary>
        /// If a small cave has been visited twice in this traversal already.
        /// </summary>
        bool CanRevisitSmallCave { get; set; }

        /// <summary>
        /// The path so far.
        /// </summary>
        Stack<string> PathSoFar { get; set; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var inputLines = Input.SplitLines();
            Connections = new Dictionary<string, List<string>>();
            PathSoFar = new Stack<string>();

            foreach(var line in inputLines)
            {
                var caves = line.Split('-');
                AddConnection(caves[0], caves[1]);
                AddConnection(caves[1], caves[0]);
            }
        }

        /// <summary>
        /// Add a connection.
        /// </summary>
        /// <param name="from">The cave this is going from.</param>
        /// <param name="to">The cave this is going to.</param>
        protected void AddConnection(string from, string to)
        {
            // Don't add the start cave as a destination as we never want to return there.
            if (to == START_CAVE) return;
            if (!Connections.ContainsKey(from))
            {
                Connections.Add(from, new List<string>());
            }
            Connections[from].Add(to);
        }

        /// <summary>
        /// Find the number of paths from the current position.
        /// </summary>
        /// <returns>The number of paths.</returns>
        public int FindPaths()
        {
            var currentSpot = PathSoFar.Peek();
            if (currentSpot == END_CAVE) {
                return 1;
            }

            int paths = 0;

            foreach(var dest in Connections[currentSpot])
            {
                var isLargeCave = char.IsUpper(dest[0]);
                var isSecondVisit = !isLargeCave && PathSoFar.Contains(dest);
                if (isLargeCave || !isSecondVisit || CanRevisitSmallCave)
                {
                    if(isSecondVisit) CanRevisitSmallCave = false;
                    PathSoFar.Push(dest);

                    paths += FindPaths();
                    
                    PathSoFar.Pop();
                    if (isSecondVisit) CanRevisitSmallCave = true;
                }
            }

            return paths;
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            CanRevisitSmallCave = false;
            PathSoFar = new Stack<string>();
            PathSoFar.Push(START_CAVE);
            return FindPaths();
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            CanRevisitSmallCave = true;
            PathSoFar = new Stack<string>();
            PathSoFar.Push(START_CAVE);
            return FindPaths();
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
