using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day4
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 4;

        /// <summary>
        /// The input lines
        /// </summary>
        protected string[] InputLines { get; }

        /// <summary>
        /// The numbers to be drawn.
        /// </summary>
        protected int[] DrawnNumbers { get; }

        /// <summary>
        /// The boards.
        /// </summary>
        protected Board[] Boards { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            InputLines = Input.SplitLines();

            var nums = InputLines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
            DrawnNumbers = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                DrawnNumbers[i] = int.Parse(nums[i]);
            }

            int pos = 2;
            List<Board> boards = new List<Board>();

            while (pos < InputLines.Length)
            {
                boards.Add(new Board(InputLines.Skip(pos).Take(5).ToArray()));
                pos += 6;
            }

            Boards = boards.ToArray();
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            foreach(var num in DrawnNumbers)
            {
                foreach(Board board in Boards)
                {
                    if (board.Mark(num))
                        return board.SumUnmarked() * num;
                }
            }
            return 0;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            List<Board> boards = new(Boards);
            foreach (var num in DrawnNumbers)
            {
                int i = 0;
                while (i < boards.Count)
                {
                    if (boards[i].Mark(num))
                    {
                        if (boards.Count > 1)
                            boards.Remove(boards[i]);
                        else
                            return boards[i].SumUnmarked() * num;
                    }
                    else
                        ++i;
                }
            }
            return 0;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
