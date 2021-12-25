using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day25
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 25;

        /// <summary>
        /// The current state of the sea floor.
        /// </summary>
        public char[,] SeaFloor { get; set; }
        /// <summary>
        /// The width of the sea floor area.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// The height of the sea floor area.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// East moving sea cucumber.
        /// </summary>
        const char EAST = '>';

        /// <summary>
        /// South moving sea cucumber
        /// </summary>
        const char SOUTH = 'v';

        /// <summary>
        /// An empty space.
        /// </summary>
        const char EMPTY = '.';

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var lines = Input.SplitLines();
            Width = lines[0].Length;
            Height = lines.Length;

            SeaFloor = new char[Width, Height];

            for(int y = 0; y < lines.Length; y++)
            {
                for(int x = 0; x < lines[y].Length; x++)
                {
                    SeaFloor[x, y] = lines[y][x];
                }
            }
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            int step = 0;
            bool somethingMoved = true;
            while (somethingMoved)
            {
                step++;
                somethingMoved = false;
                var newSeaFloor = new char[Width, Height];

                // Initialize the new array to empty spaces.
                newSeaFloor.ForEach((x, y) => { newSeaFloor[x, y] = EMPTY; });

                // Move east first.
                SeaFloor.ForEach((x, y) =>
                {
                    var nextX = x < Width - 1 ? x + 1 : 0;
                    if (SeaFloor[x, y] == EAST)
                    {
                        if (SeaFloor[nextX, y] == EMPTY)
                        {
                            newSeaFloor[nextX, y] = EAST;
                            somethingMoved = true;
                        }
                        else
                            newSeaFloor[x, y] = EAST;
                    }
                        
                });

                // Move south.
                SeaFloor.ForEach((x, y) =>
                {
                    var nextY = y < Height - 1 ? y + 1 : 0;
                    if (SeaFloor[x, y] == SOUTH)
                    {
                        if (SeaFloor[x, nextY] != SOUTH && newSeaFloor[x, nextY] == EMPTY)
                        {
                            newSeaFloor[x, nextY] = SOUTH;
                            somethingMoved = true;
                        }
                        else
                            newSeaFloor[x, y] = SOUTH;
                    }
                    
                });

                SeaFloor = newSeaFloor;
            }
            return step;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            // Part 2 is a gimme.
            return 0;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
