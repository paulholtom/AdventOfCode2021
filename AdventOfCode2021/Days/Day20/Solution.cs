using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day20
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 20;

        /// <summary>
        /// The algorithm.
        /// </summary>
        public string Algorithm { get; }
        /// <summary>
        /// The current image.
        /// </summary>
        public char[,] Image { get; set; }
        /// <summary>
        /// The current iteration.
        /// </summary>
        public int Iteration { get; set; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var lines = Input.SplitLines();

            Algorithm = lines[0];

            // The first two lines are the Algorithm then a blank line. The rest is the image.
            Image = new char[lines[2].Length, lines.Length - 2];
            for (int y = 2; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    Image[x, y - 2] = lines[y][x];
                }
            }
        }

        /// <summary>
        /// Get the value at the specified position.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <returns>True if that is on, false if it's off.</returns>
        public bool ValueAtPosition(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Image.GetLength(0) || y >= Image.GetLength(0))
            {
                /**
                 * The outer pixels will be one for any of these situations:
                 * - The first iteration
                 * - Position 0 of the algorithm is off.
                 * - Poistion 0 is on, The last position is off and this is an even iteration
                 */
                if (Iteration == 0 ||
                    Algorithm[0] == '.' || 
                    (Algorithm[^1] == '.' && Iteration % 2 == 0)) 
                    return false;
                // The outer pixels will be on in all other cases.
                else 
                    return true;
            }
            return Image[x, y] == '#';
        }

        /// <summary>
        /// Run the algorithm against the current image once.
        /// </summary>
        public void RunAlgorithm()
        {
            var newImage = new char[Image.GetLength(0) + 2, Image.GetLength(1) + 2];

            // Determine the value of each pixel in the new image.
            for (int x = 0; x < newImage.GetLength(0); ++x)
            {
                for (int y = 0; y < newImage.GetLength(1); ++y)
                {
                    int index = 0;
                    // These inner positions are on the original image so there's an additional -1 since the new image has grown.
                    for (int yInner = y - 2; yInner <= y; ++yInner)
                    {
                        for (int xInner = x - 2; xInner <= x; ++xInner)
                        {
                            // Add either a 0 or 1 as the last bit by shifting everything over then doing a bitwise or (if it should be 1)
                            index <<= 1;
                            if (ValueAtPosition(xInner, yInner)) index |= 1;
                        }
                    }
                    newImage[x, y] = Algorithm[index];
                }
            }

            Image = newImage;
            ++Iteration;
        }

        /// <summary>
        /// Count the number of 
        /// </summary>
        /// <returns></returns>
        public int CountLitPixels()
        {
            int count = 0;
            Image.ForEach((x, y) =>
            {
                if (Image[x, y] == '#') count++;
            });
            return count;
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            while(Iteration < 2)
            {
                RunAlgorithm();
            }

            return CountLitPixels();
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            while (Iteration < 50)
            {
                RunAlgorithm();
            }
            
            return CountLitPixels();
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
