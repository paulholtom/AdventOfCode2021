using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    /// <summary>
    /// Common 2d array tasks
    /// </summary>
    public static class Array2d
    {
        /// <summary>
        /// Perform an action on each element in a 2d array.
        /// </summary>
        /// <typeparam name="Type">The types of elements in the array.</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="action">The action to perform.</param>
        public static void ForEach<Type>(this Type[,] array, Action<int, int> action)
        {
            for(int x = 0; x < array.GetLength(0); x++)
            {
                for(int y = 0; y < array.GetLength(1); y++)
                {
                    action(x, y);
                }
            }
        }

        /// <summary>
        /// Perform an action for each adjacent point in a 2d array.
        /// </summary>
        /// <typeparam name="Type">The type of elements in the array.</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="x">The original point's x coordinate.</param>
        /// <param name="y">The original point's y coordinate.</param>
        /// <param name="includeDiagonals">If diagonals should be included.</param>
        /// <param name="action">The action.</param>
        public static void ForEachAdjacent<Type>(this Type[,] array, int x, int y, bool includeDiagonals, Action<int, int> action)
        {
            for (int xInner = x - 1; xInner <= x + 1; xInner++)
            {
                for (int yInner = y - 1; yInner <= y + 1; yInner++)
                {
                    if (xInner >= 0 &&
                        yInner >= 0 &&
                        xInner < array.GetLength(0) &&
                        yInner < array.GetLength(1) &&
                        (includeDiagonals || xInner == 0 || yInner == 0) && // Either include diagonals or make sure this point is orthogonal.
                        !(xInner == x && yInner == y) // Don't do the action for the original point.
                        )
                        action(xInner, yInner);
                }
            }
        }
    }
}
