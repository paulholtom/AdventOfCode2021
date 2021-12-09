using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day9
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 9;

        /// <summary>
        /// The heights.
        /// </summary>
        protected int[,] Heights { get; }

        /// <summary>
        /// Points that have been in included in a basin already.
        /// </summary>
        protected bool[,] InBasin { get; }

        /// <summary>
        /// The low points.
        /// </summary>
        protected Point[] LowPoints { get; }

        /// <summary>
        /// The maximum X position.
        /// </summary>
        int MaxX { get; }
        /// <summary>
        /// The maximum Y position.
        /// </summary>
        int MaxY { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var lines = Input.SplitLines();
            Heights = new int[lines[0].Length, lines.Length];
            for(int y = 0; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    Heights[x,y] = lines[y][x] - 48;
                }
            }

            List<Point> points = new List<Point>();
            MaxX = Heights.GetLength(0) - 1;
            MaxY = Heights.GetLength(1) - 1;
            for (int x = 0; x <= MaxX; ++x)
            {
                for (int y = 0; y <= MaxY; ++y)
                {
                    var point = new Point(x, y);
                    if (IsLowPoint(point))
                    {
                        points.Add(point);
                    }
                }
            }
            LowPoints = points.ToArray();

            InBasin = new bool[MaxX + 1, MaxY + 1];
        }

        /// <summary>
        /// If the provided point is the lowest local point.
        /// </summary>
        /// <param name="p">The point to check</param>
        /// <returns>True if all adjacent points are higher, false otherwise.</returns>
        protected bool IsLowPoint(Point p)
        {
            int height = Heights[p.X, p.Y];
            foreach(var adjacent in GetAdjacentPoints(p))
            {
                if (Heights[adjacent.X, adjacent.Y] <= height)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get all of the points adjacent to the provided point.
        /// </summary>
        /// <param name="p">The point to check.</param>
        /// <returns>An array of adjacent points.</returns>
        protected Point[] GetAdjacentPoints(Point p)
        {
            List<Point> points = new();

            if (p.X != 0) points.Add(new Point(p.X - 1, p.Y));
            if (p.Y != 0) points.Add(new Point(p.X, p.Y - 1));
            if (p.X != MaxX) points.Add(new Point(p.X + 1, p.Y));
            if (p.Y != MaxY) points.Add(new Point(p.X, p.Y + 1));

            return points.ToArray();
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            int total = 0;
            foreach(var point in LowPoints)
            {
                total += Heights[point.X, point.Y] + 1;
            }
            return total;
        }

        /// <summary>
        /// Get the number of higher points adjacent to this point that aren't already in a basin.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>The number of higher points not already in a basin.</returns>
        protected int GetHigherPointsNotInBasin(Point point)
        {
            int size = 1;
            int currentHeight = Heights[point.X, point.Y];
            InBasin[point.X, point.Y] = true;
            if (currentHeight == 8) return size;
            foreach(var p in GetAdjacentPoints(point))
            {
                var adjacentHeight = Heights[p.X, p.Y];
                if (adjacentHeight < 9 && adjacentHeight > currentHeight && !InBasin[p.X, p.Y])
                    size += GetHigherPointsNotInBasin(p);
            }
            return size;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            int[] basinSizes = new int[LowPoints.Length];

            for(int i = 0; i < basinSizes.Length; ++i)
            {
                basinSizes[i] = GetHigherPointsNotInBasin(LowPoints[i]);
            }
            return basinSizes.OrderBy(x => -x).Take(3).Aggregate(1, (a, b) => a * b);
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
