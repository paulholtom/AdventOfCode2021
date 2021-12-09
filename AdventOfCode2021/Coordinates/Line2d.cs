using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AdventOfCode2021.Coordinates
{
    /// <summary>
    /// A line.
    /// </summary>
    public class Line2d
    {
        /// <summary>
        /// The start of the line.
        /// </summary>
        protected Point Start { get; }
        /// <summary>
        /// The end of the line.
        /// </summary>
        protected Point End { get; }
        /// <summary>
        /// All points in the line.
        /// </summary>
        public Point[] Points { get; }

        /// <summary>
        /// The separator for the two points in a line.
        /// </summary>
        public const string LINE_SEPARATOR = " -> ";
        /// <summary>
        /// The separator for the x and y cooridinates in a string.
        /// </summary>
        public const char POINT_SEPARATOR = ',';

        /// <summary>
        /// Constructor that will parse a string input.
        /// </summary>
        /// <param name="input">The string input for the line.</param>
        public Line2d(string input)
        {
            var split = input.Split(LINE_SEPARATOR);

            Start = ParsePoint(split[0]);
            End = ParsePoint(split[1]);

            Points = GetAllPoints();
        }

        /// <summary>
        /// Constructor that takes start and end points.
        /// </summary>
        /// <param name="input">The string input for the line.</param>
        public Line2d(Point start, Point end)
        {
            Start = start;
            End = end;

            Points = GetAllPoints();
        }

        /// <summary>
        /// Parse a single point.
        /// </summary>
        /// <param name="point">The point string.</param>
        /// <returns>The parsed point.</returns>
        protected Point ParsePoint(string point)
        {
            var pointSplit = point.Split(POINT_SEPARATOR);
            return new Point(int.Parse(pointSplit[0]), int.Parse(pointSplit[1]));
        }

        /// <summary>
        /// If this line is horizontal.
        /// </summary>
        public bool IsHorizontal
        {
            get
            {
                return Start.Y == End.Y;
            }
        }

        /// <summary>
        /// If this line is vertical.
        /// </summary>
        public bool IsVertical
        {
            get
            {
                return Start.X == End.X;
            }
        }

        /// <summary>
        /// Get all of the points in the line.
        /// </summary>
        /// <returns>All of the points.</returns>
        protected Point[] GetAllPoints()
        {
            int xStep = Start.X == End.X ? 0 : (Start.X < End.X ? 1 : -1);
            int yStep = Start.Y == End.Y ? 0 : (Start.Y < End.Y ? 1 : -1);

            List<Point> points = new List<Point>();
            int x = Start.X;
            int y = Start.Y;

            while (x != End.X || y != End.Y)
            {
                points.Add(new Point(x, y));
                x += xStep;
                y += yStep;
            }

            // Add the last point.
            points.Add(new Point(x, y));
            
            return points.ToArray();
        }
    }
}
