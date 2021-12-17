using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day17
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 17;

        /// <summary>
        /// The minimum X position for the target window.
        /// </summary>
        int MinX { get; }
        /// <summary>
        /// The maximum X position for the target window.
        /// </summary>
        int MaxX { get; }
        /// <summary>
        /// The minimum Y position for the target window.
        /// </summary>
        int MinY { get; }
        /// <summary>
        /// The maximum Y position for the target window.
        /// </summary>
        int MaxY { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var splitXY = Input.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var xVals = splitXY[0].Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries)[1].Split("..");
            MinX = int.Parse(xVals[0]);
            MaxX = int.Parse(xVals[1]);
            var yVals = splitXY[1].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1].Split("..");
            MinY = int.Parse(yVals[0]);
            MaxY = int.Parse(yVals[1]);
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            // This assumes that there's always some X velocity that will be in the window at an appropriate time for any Y velocity.
            // This also assumes the Y window is in the negatives.
            // Any positive Y velocity will reach an apex of the sum of all numbers up to it's initial velocity. (n(n+1)/2)
            // They will also always come back down to 0 with a velocity of -(initial + 1)
            // So anything more than |MinY| - 1 will overshoot, so that's the initial velocity that gives the maximum height.
            // Assuming MinY < -1: (|MinY|-1)(|MinY|-1+1)/2 = MinY(MinY + 1)/2
            return MinY * (MinY + 1) / 2;
        }

        /// <summary>
        /// Get the initial velocities that will stop in the target window.
        /// </summary>
        /// <param name="minVel">The minimum velocity to check.</param>
        /// <param name="maxVel">The maximum velocity to check.</param>
        /// <param name="minWindow">The minimum value for the window.</param>
        /// <param name="maxWindow">The maximum value for the window.</param>
        /// <param name="stopsAtZero">If this should stop checking when the velocity reaches 0.</param>
        /// <returns>An array of velocities that are inside the target window and the number of steps where it happens for each.</returns>
        protected VelocityInfo[] GetVelocities(int minVel, int maxVel, int minWindow, int maxWindow, bool stopsAtZero)
        {
            var velocities = new List<VelocityInfo>();
            for (int i = minVel; i <= maxVel; ++i)
            {
                int currentVelocity = i;
                int currentPosition = 0;
                int step = 0;
                var steps = new List<int>();

                while ((!stopsAtZero && currentPosition >= minWindow) || // Stopping condition for y velocities.
                    (stopsAtZero && currentPosition <= maxWindow && currentVelocity > 0)) // Stopping condition for x velocities.
                {
                    ++step;
                    currentPosition += currentVelocity;
                    currentVelocity--;
                    if (currentPosition >= minWindow && currentPosition <= maxWindow)
                    {
                        steps.Add(step);
                    }
                }

                if (steps.Count > 0)
                {
                    velocities.Add(new VelocityInfo(i, steps.ToArray(), stopsAtZero && currentVelocity == 0 && currentPosition <= maxWindow));
                }
            }
            return velocities.ToArray();
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            var yVelocities = GetVelocities(MinY, Math.Abs(MinY), MinY, MaxY, false);

            var xVelocities = GetVelocities(1, MaxX, MinX, MaxX, true);

            int count = 0;

            foreach(var x in xVelocities)
            {
                foreach(var y in yVelocities)
                {
                    if ((x.Stops && y.Steps.Min() >= x.Steps.Min()) // If the x velocity stops in the window then the valid steps are actually everything after it reaches the window.
                        || y.Steps.Intersect(x.Steps).Any())
                        count++;
                }
            }

            return count;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
