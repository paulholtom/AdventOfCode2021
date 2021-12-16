using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day15
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 15;

        /// <summary>
        /// The risk levels at each point.
        /// </summary>
        protected int[,] Risks { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var inputLines = Input.SplitLines();
            Risks = new int[inputLines[0].Length, inputLines.Length];
            for(int y = 0; y < inputLines.Length; y++)
            {
                for(int x = 0; x < inputLines[y].Length; x++)
                {
                    Risks[x, y] = inputLines[y][x] - '0';
                }
            }
        }

        /// <summary>
        /// Find the total risk of the best path.
        /// </summary>
        /// <param name="risks">The risks/distances for each position.</param>
        /// <returns>The total risk for the best bath.</returns>
        public int FindBestPath(int [,] risks)
        {
            // The length of the best path to each point and the previous point on that path.
            var bestPaths = new Dictionary<Point, int>();
            var previousPoint = new Dictionary<Point, Point>();
            risks.ForEach((x, y) =>
            {
                var p = new Point(x, y);
                // The best paths found so far.
                bestPaths[p] = int.MaxValue;
            });
            bestPaths[new Point(0, 0)] = 0;

            Dictionary<Point, int> pending = new();
            pending.Add(new Point(0, 0), 0);

            while (pending.Count > 0)
            {
                var currentPoint = pending.OrderBy(p => p.Value).First().Key;
                pending.Remove(currentPoint);

                risks.ForEachAdjacent(currentPoint.X, currentPoint.Y, false, (x, y) =>
                {
                    var dest = new Point(x, y);
                    var distance = bestPaths[currentPoint] + risks[x, y];
                    if (distance < bestPaths[dest])
                    {
                        pending[dest] = bestPaths[dest] = distance;
                        previousPoint[dest] = currentPoint;
                    }
                });
            }

            return bestPaths[new Point(risks.GetLength(0) - 1, risks.GetLength(1) - 1)];
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public long RunPart1()
        {
            return FindBestPath(Risks);
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            var origWidth = Risks.GetLength(0);
            var origHeight = Risks.GetLength(1);
            var risks = new int[Risks.GetLength(0) * 5, Risks.GetLength(1) * 5];
            risks.ForEach((x, y) =>
            {
                var origX = x % origWidth;
                var origY = y % origHeight;
                var multipleX = (int)Math.Floor((double)x / origWidth);
                var multipleY = (int)Math.Floor((double)y / origHeight);

                var val = Risks[origX, origY] + multipleX + multipleY;
                while (val > 9)
                {
                    val -= 9;
                }

                risks[x, y] = val;
            });
            
            var width = origWidth * 5;
            var height = origHeight * 5;
            return FindBestPath(risks);
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
