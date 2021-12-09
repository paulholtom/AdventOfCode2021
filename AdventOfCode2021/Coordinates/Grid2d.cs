using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Coordinates
{
    /// <summary>
    /// A grid that counts how many times lines cross each point on it.
    /// </summary>
    public class Grid2d
    {
        /// <summary>
        /// The points on the grid.
        /// </summary>
        public List<List<int>> Points { get; } = new();

        /// <summary>
        /// "Draw" a line to the grid.
        /// </summary>
        /// <param name="line">The line to draw.</param>
        public void DrawLine(Line2d line)
        {
            foreach (var point in line.Points)
            {
                // If the grid hasn't grown the required X coordinate, grow it.
                if (Points.Count() <= point.X)
                {
                    for (int i = Points.Count(); i <= point.X; ++i)
                    {
                        Points.Add(new());
                    }
                }
                // If this line in the grid hasn't grown to the Y coordinate, grow it.
                if (Points[point.X].Count() <= point.Y)
                {
                    for (int i = Points[point.X].Count(); i <= point.Y; ++i)
                    {
                        Points[point.X].Add(0);
                    }
                }

                // Increase the count at this point in the grid.
                Points[point.X][point.Y]++;
            }
        }

        /// <summary>
        /// Count the number of places where lines overlap.
        /// </summary>
        /// <returns>The total number of points where lines overlap.</returns>
        public int CountOverlaps()
        {
            int total = 0;
            foreach (var gridLine in Points)
            {
                foreach (var point in gridLine)
                {
                    if (point > 1)
                        ++total;
                }
            }
            return total;
        }
    }
}
