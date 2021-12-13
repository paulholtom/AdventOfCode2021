using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day13
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 13;

        /// <summary>
        /// The folds to be done.
        /// </summary>
        List<string> Folds { get; }
        /// <summary>
        /// The initial points.
        /// </summary>
        List<Point> Points { get; }
        /// <summary>
        /// The max x coordinate
        /// </summary>
        int MaxX { get; set; }
        /// <summary>
        /// The max y coordinate.
        /// </summary>
        int MaxY { get; set; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var inputLines = Input.SplitLines();

            int i = 0;
            MaxX = 0;
            MaxY = 0;
            Points = new List<Point>();
            while (!string.IsNullOrEmpty(inputLines[i]))
            {
                var coords = inputLines[i].Split(',');
                var point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                Points.Add(point);
                if(point.X > MaxX) MaxX = point.X;
                if(point.Y > MaxY) MaxY = point.Y;
                ++i;
            }

            Folds = new List<string>();
            ++i;
            while (i < inputLines.Length)
            {
                Folds.Add(inputLines[i].Replace("fold along ", ""));
                i++;
            }
        }

        /// <summary>
        /// Perform a single fold.
        /// </summary>
        /// <param name="fold">Information about the line to fold at.</param>
        /// <param name="points">The points before the fold.</param>
        /// <returns>The points after the fold.</returns>
        protected List<Point> DoFold(string fold, List<Point> points)
        {
            var foldSplit = fold.Split('=');
            var lineNumber = int.Parse(foldSplit[1]);
            var newPoints = new List<Point>();

            bool onX = foldSplit[0] == "x";

            // After the fold the max in that dimension is whatever line the fold happened at.
            if (onX) MaxX = lineNumber;
            else MaxY = lineNumber;

            foreach(var point in points)
            {
                var x = point.X;
                var y = point.Y;
                if(onX)
                {
                    if(x > lineNumber)
                    {
                        x -= 2 * (x - lineNumber);
                    }
                }
                else
                {
                    if (y > lineNumber)
                    {
                        y -= 2 * (y - lineNumber);
                    }
                }
                var newPoint = new Point(x, y);
                if (!newPoints.Contains(newPoint)) { newPoints.Add(newPoint); }
            }
            return newPoints;
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            return DoFold(Folds[0], Points).Count;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public string RunPart2()
        {
            var points = Points;
            foreach(var fold in Folds)
            {
                points = DoFold(fold, points);
            }

            // Build the string to display the results.
            StringBuilder display = new StringBuilder();
            for(int y = 0; y< MaxY; y++)
            {
                for(int x = 0; x < MaxX; x++)
                {
                    if (points.Contains(new Point(x, y)))
                    {
                        display.Append("#");
                    }
                    else
                    {
                        display.Append(".");
                    }
                }
                display.AppendLine();
            }
            return display.ToString();
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
