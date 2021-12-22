using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day22
{
    /// <summary>
    /// Tracks what parts of the reactor are on.
    /// </summary>
    public class ReactorState
    {
        /// <summary>
        /// The list of cubiods showing which parts are on.
        /// </summary>
        public List<Cuboid> Cuboids { get; set; }

        /// <summary>
        /// Constructor.
        /// Starts with everything off.
        /// </summary>
        public ReactorState()
        {
            Cuboids = new();
        }

        /// <summary>
        /// Apply a cuboid. Turning all points in that cuboid on or off to match the state specified in the cuboid.
        /// </summary>
        /// <param name="toApply">The cuboid to apply.</param>
        public void ApplyCuboid(Cuboid toApply)
        {
            var newCubiods = new List<Cuboid>();
            foreach(var cuboid in Cuboids)
            {
                newCubiods.AddRange(cuboid.GetNonOverlapping(toApply));
            }
            if(toApply.On) newCubiods.Add(toApply);
            Cuboids = newCubiods;
        }

        /// <summary>
        /// Get the number of parts that are on.
        /// </summary>
        /// <returns>The count of points that are on.</returns>
        public long GetOnCount()
        {
            return Cuboids.Sum(c => c.GetSize());
        }
    }
}
