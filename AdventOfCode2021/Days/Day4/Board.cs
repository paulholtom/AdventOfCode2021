using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day4
{
    public class Board
    {
        /// <summary>
        /// The numbers
        /// </summary>
        protected int[,] Numbers { get; }
        /// <summary>
        /// The numbers that have been marked.
        /// </summary>
        protected bool [,] Marked { get; }
        /// <summary>
        /// If the board is complete.
        /// </summary>
        protected bool Complete { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">The input.</param>
        public Board(string[] input)
        {
            Numbers = new int[5,5];
            Marked = new bool[5,5];
            Complete = false;

            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                for (int j = 0; j < Numbers.GetLength(1); j++)
                {
                    Marked[i, j] = false;
                }
            }

            for (int i = 0; i < input.Length; i++)
            {
                var nums = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for(int j = 0; j < nums.Length; ++j)
                {
                    Numbers[i, j] = int.Parse(nums[j]);
                }
            }
        }

        /// <summary>
        /// Mark a number.
        /// </summary>
        /// <param name="number">The number</param>
        /// <returns>True if the board completed.</returns>
        public bool Mark(int number)
        {
            for(int i = 0;i < Numbers.GetLength(0); i++)
            {
                for(int j = 0; j < Numbers.GetLength(1); j++)
                {
                    if(Numbers[i, j] == number)
                        Marked[i, j] = true;
                }
            }
            Complete = Completed();
            return Complete;
        }

        /// <summary>
        /// If a row is complete.
        /// </summary>
        /// <param name="row">The row to check.</param>
        /// <returns>True if the row is complete.</returns>
        protected bool RowComplete(int row) 
        { 
            for(int i = 0; i < Numbers.GetLength(0); i++)
            {
                if (!Marked[row, i]) return false;
            }
            return true;
        }

        /// <summary>
        /// If the column is complete.
        /// </summary>
        /// <param name="column">The column to check.</param>
        /// <returns>True if the column is complete.</returns>
        protected bool ColumnComplete(int column)
        {
            for (int i = 0; i < Numbers.GetLength(1); i++)
            {
                if (!Marked[i, column]) return false;
            }
            return true;
        }

        /// <summary>
        /// If this board is completed
        /// </summary>
        /// <returns>True if the board is complete</returns>
        protected bool Completed()
        {
            for (int i = 0; i < 5; i++)
            {
                if(RowComplete(i) || ColumnComplete(i)) return true;
            }
            return false;
        }

        /// <summary>
        /// Get the sum of the unmarked numbers.
        /// </summary>
        /// <returns>The sum.</returns>
        public int SumUnmarked()
        {
            int sum = 0;
            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                for (int j = 0; j < Numbers.GetLength(1); j++)
                {
                    if (!Marked[i, j]) sum += Numbers[i,j];
                }
            }
            return sum;
        }
    }
}
