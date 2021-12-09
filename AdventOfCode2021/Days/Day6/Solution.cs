using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day6
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 6;

        /// <summary>
        /// The starting count of each type of fish.
        /// </summary>
        protected long[] Fish;

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            List<int> fishList = new(); 
            foreach(var fish in Input.Split(','))
            {
                fishList.Add(int.Parse(fish));
            }
            Fish = new long[9]; 
            foreach(var fish in fishList)
            {
                Fish[fish]++;
            }
        }

        /// <summary>
        /// Simulate the provided fish spawning for the provided number days.
        /// </summary>
        /// <param name="input">The count of each type of fish.</param>
        /// <param name="days">The number of days they spawn for.</param>
        /// <returns>The number of fish after the number of days provided.</returns>
        public long Simulate(long[] input, int days)
        {
            long[] fish = (long[])input.Clone();

            for(int i = 0; i < days; i++)
            {
                var count0 = fish[0];
                for(int j = 0; j < fish.Length - 1; j++)
                {
                    fish[j] = fish[j + 1];
                }
                // The ones that were at 0 go to 6, in addition to ones from 7
                fish[6] += count0;
                // The ones that were at 0 spawn new ones at 8, nothing else moves here so set instead of add to the number.
                fish[fish.Length - 1] = count0;
            }

            return fish.Sum();
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public long RunPart1()
        {
            return Simulate(Fish, 80);
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            return Simulate(Fish, 256);
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
