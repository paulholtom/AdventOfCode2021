using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day11
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 11;

        /// <summary>
        /// The octopi.
        /// </summary>
        protected int[,] Octopi { get; }

        /// <summary>
        /// The number of flashes after 100 steps.
        /// </summary>
        protected int FlashesIn100 { get; }

        /// <summary>
        /// The first step where all of the octopi flash.
        /// </summary>
        protected int FirstStepAllFlash { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var inputLines = Input.SplitLines();

            Octopi = new int[10, 10];
            for (int y = 0; y < inputLines.Length; ++y)
            {
                for (int x = 0; x < inputLines[y].Length; ++x)
                {
                    Octopi[x, y] = inputLines[x][y] - '0';
                }
            }

            bool foundAllFlash = false;
            FlashesIn100 = 0;
            for (int i = 0; i < 100; ++i)
            {
                var flashes = RunStep();
                FlashesIn100 += flashes;

                // Check if the first point they all flash is in the first 100.
                if(flashes == 100 && !foundAllFlash)
                    FirstStepAllFlash = i + 1;
            }

            if (!foundAllFlash)
            {
                // If the point where they all flash hasn't been reached, continue until it is reached.
                FirstStepAllFlash = 101;
                while (RunStep() != 100)
                {
                    FirstStepAllFlash++;
                }
            }
        }

        /// <summary>
        /// Run one step.
        /// </summary>
        /// <returns>The number of flashes in the step.</returns>
        protected int RunStep()
        {
            // Increase all of them by one.
            Octopi.ForEach((x, y) => Octopi[x, y]++);

            // Handle flashes from each of them.
            int totalFlashes = 0;
            bool newFlashes;
            bool[,] alreadyFlashed = new bool[Octopi.GetLength(0), Octopi.GetLength(1)];
            do
            {
                newFlashes = false;

                Octopi.ForEach((x, y) =>
                {
                    if (Octopi[x, y] > 9 && !alreadyFlashed[x, y])
                    {
                        newFlashes = true;
                        alreadyFlashed[x, y] = true;
                        totalFlashes++;

                        Octopi.ForEachAdjacent(x, y, true, (xInner, yInner) =>
                        {
                            Octopi[xInner, yInner]++;
                        });
                    }
                });
            } while (newFlashes);

            // Set all of the flashed octopi back to 0.
            Octopi.ForEach((x, y) =>
            {
                if (Octopi[x, y] > 9)
                    Octopi[x, y] = 0;
            });
            
            return totalFlashes;
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            return FlashesIn100;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            return FirstStepAllFlash;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
