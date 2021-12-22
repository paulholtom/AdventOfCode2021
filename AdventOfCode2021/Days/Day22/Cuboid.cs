using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day22
{
    /// <summary>
    /// A representation of a group of points in 3d space.
    /// </summary>
    public class Cuboid
    {
        /// <summary>
        /// If this is on (true) or off (false).
        /// </summary>
        public bool On { get; }
        /// <summary>
        /// The minimum X coordinate.
        /// </summary>
        public int MinX { get; }
        /// <summary>
        /// The maximum X coordinate.
        /// </summary>
        public int MaxX { get; }
        /// <summary>
        /// The minimum Y coordinate.
        /// </summary>
        public int MinY { get; }
        /// <summary>
        /// The maximum Y coordinate.
        /// </summary>
        public int MaxY { get; }
        /// <summary>
        /// The minimum Z coordinate.
        /// </summary>
        public int MinZ { get; }
        /// <summary>
        /// The maximum Z coordinate.
        /// </summary>
        public int MaxZ { get; }

        /// <summary>
        /// Constructor from a string.
        /// </summary>
        /// <param name="input">The string.</param>
        public Cuboid(string input)
        {
            var firstSplit = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            On = firstSplit[0] == "on";

            var coordSplit = firstSplit[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            (MinX, MaxX) = GetMinMax(coordSplit[0]);
            (MinY, MaxY) = GetMinMax(coordSplit[1]);
            (MinZ, MaxZ) = GetMinMax(coordSplit[2]);
        }

        /// <summary>
        /// Get the min and max values from a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The min and max values.</returns>
        protected (int min, int max) GetMinMax(string input)
        {
            var nums = input.Split('=')[1].Split("..");
            return (int.Parse(nums[0]), int.Parse(nums[1]));
        }

        /// <summary>
        /// Constructor from basic values.
        /// </summary>
        /// <param name="on">If this cubiod is on (true) or off (false)</param>
        /// <param name="minX">Minimum X coordinate.</param>
        /// <param name="maxX">Maximum X coordinate.</param>
        /// <param name="minY">Minimum Y coordinate.</param>
        /// <param name="maxY">Maximum Y coordinate.</param>
        /// <param name="minZ">Minimum Z coordinate.</param>
        /// <param name="maxZ">Maximum Z coordinate.</param>
        public Cuboid(bool on, int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
        {
            On = on;
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            MinZ = minZ;
            MaxZ = maxZ;
        }

        /// <summary>
        /// Break this cubiod into other cubiods that don't overlap with the specified cuboid.
        /// 
        /// The new cubiods will have the same on state as this cuboid.
        /// </summary>
        /// <param name="other">The other cubiod.</param>
        /// <returns>
        /// Non overlapping cubiods.
        /// If these overlap entirely this will be an empty array.
        /// If they don't overlap at all this will return itself as the only element of the array.
        /// </returns>
        public Cuboid[] GetNonOverlapping(Cuboid other)
        {
            // Check if there's no overlap at all first.
            if (MaxX < other.MinX || 
                MinX > other.MaxX || 
                MaxY < other.MinY ||
                MinY > other.MaxY ||
                MaxZ < other.MinZ ||
                MinZ > other.MaxZ)
                return new Cuboid[] { this };

            // Check if this is entirely contained in the other cuboid.
            if (MinX >= other.MinX && MaxX <= other.MaxX && MinY >= other.MinY && MaxY <= other.MaxY && MinZ >= other.MinZ && MaxZ <= other.MaxZ)
                return new Cuboid[0];

            List<Cuboid> result = new List<Cuboid>();

            if(MinX < other.MinX)
            {
                // Include everything with a lower X value into a single cuboid.
                result.Add(new Cuboid(On, MinX, other.MinX - 1, MinY, MaxY, MinZ, MaxZ));
            }
            if (MaxX > other.MaxX)
            {
                // Include everything with a greater X value into a single cuboid.
                result.Add(new Cuboid(On, other.MaxX + 1, MaxX, MinY, MaxY, MinZ, MaxZ));
            }
            // Other cuboids should not include any X values outside either cuboids min/max values as those points have all been counted already.
            var newMinX = Math.Max(MinX, other.MinX);
            var newMaxX = Math.Min(MaxX, other.MaxX);
            if (MinY < other.MinY)
            {
                // Include everything with a lower Y value.
                result.Add(new Cuboid(On, newMinX, newMaxX, MinY, other.MinY - 1, MinZ, MaxZ));
            }
            if (MaxY > other.MaxY)
            {
                // Include everything with a higher Y value.
                result.Add(new Cuboid(On, newMinX, newMaxX, other.MaxY + 1, MaxY, MinZ, MaxZ));
            }
            // Other cuboids should not include any Y values outside either cuboids min/max values as those points have all been counted already.
            var newMinY = Math.Max(MinY, other.MinY);
            var newMaxY = Math.Min(MaxY, other.MaxY);
            if (MinZ < other.MinZ)
            {
                // Include everything with a lower Z value.
                result.Add(new Cuboid(On, newMinX, newMaxX, newMinY, newMaxY, MinZ, other.MinZ - 1));
            }
            if (MaxZ > other.MaxZ)
            {
                // Include everything with a higher Z value.
                result.Add(new Cuboid(On, newMinX, newMaxX, newMinY, newMaxY, other.MaxZ + 1, MaxZ));
            }

            return result.ToArray();
        }

        /// <summary>
        /// Get the number of points in this cubiod.
        /// </summary>
        /// <returns>The number of points.</returns>
        public long GetSize()
        {
            // The first part of this needs to be cast to a long before the multiplication otherwise this overflows.
            return (long)(MaxX - MinX + 1) * (MaxY - MinY + 1) * (MaxZ - MinZ + 1);
        }

        /// <summary>
        /// Create a string represenation of this cuboid.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            return $"x={MinX}..{MaxX},y={MinY}..{MaxY},z={MinZ}..{MaxZ}";
        }
    }
}
