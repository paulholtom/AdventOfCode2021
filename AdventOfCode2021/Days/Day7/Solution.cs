using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day7
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 7;

        /// <summary>
        /// The initial position of the crabs
        /// </summary>
        protected int[] Crabs { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            Crabs = Input.Split(',').Select(c => int.Parse(c)).ToArray();
        }

        /// <summary>
        /// Calculate the amount fuel for all crabs to move to a single position.
        /// </summary>
        /// <param name="pos">The postion.</param>
        /// <param name="getCost">The cost function.</param>
        /// <returns>The amount of fuel used.</returns>
        public int CalculateFuel(int pos, Func<int, int> getCost)
        {
            int total = 0;
            foreach (var c in Crabs)
            {
                total += getCost(Math.Abs(c - pos));
            }
            return total;
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            var sorted = Crabs.OrderBy(i => i);
            return CalculateFuel(sorted.ElementAt(sorted.Count() / 2), i => i);
        }

        /// <summary>
        /// The fuel cost for moving a distance using the rules for part 2.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <returns>The fuel cost.</returns>
        public int Part2FuelCost(int distance)
        {
            return distance * (distance + 1) / 2;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            //return CalculateMinFuel(Part2FuelCost);
            var mean = Crabs.Average();
            return Math.Min(
                CalculateFuel((int)Math.Floor(mean), Part2FuelCost),
                CalculateFuel((int)Math.Ceiling(mean), Part2FuelCost)
                );

        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
