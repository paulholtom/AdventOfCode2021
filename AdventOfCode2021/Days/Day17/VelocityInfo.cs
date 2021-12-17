using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day17
{
    /// <summary>
    /// Information about the results of an initial velocity.
    /// </summary>
    public class VelocityInfo
    {
        /// <summary>
        /// The initial velocity.
        /// </summary>
        public int Velocity { get; }
        /// <summary>
        /// Steps after which this will be in the target area.
        /// </summary>
        public int[] Steps { get; }
        /// <summary>
        /// If this will stop in the target area.
        /// </summary>
        public bool Stops { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="velocity">The initial velocity.</param>
        /// <param name="steps">Steps after which this will be in the target area.</param>
        /// <param name="stops">If this will stop in the target area.</param>
        public VelocityInfo(int velocity, int[] steps, bool stops = false)
        {
            Velocity = velocity;
            Steps = steps;
            Stops = stops;
        }
    }
}
