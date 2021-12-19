using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day19
{
    /// <summary>
    /// A 3D point or vector.
    /// </summary>
    public class Data3D
    {
        /// <summary>
        /// X coordinate.
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Y coordinate.
        /// </summary>
        public int Y { get; }
        /// <summary>
        /// Z coordinate.
        /// </summary>
        public int Z { get; }
        /// <summary>
        /// The magnitude, if this is considered a vector.
        /// </summary>
        public int Magnitude { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public Data3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Magnitude = Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{X},{Y},{Z}"; 
        }

        /// <summary>
        /// Gets the distance between two points.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <returns>A new point that is actually the distance.</returns>
        public static Data3D operator-(Data3D a, Data3D b)
        {
            return new Data3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Add a point to another point.
        /// 
        /// The second value being added is really just an adjustment to the first.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The adjustment.</param>
        /// <returns>A new point that is the adjusted point.</returns>
        public static Data3D operator +(Data3D a, Data3D b)
        {
            return new Data3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Overload for equals.
        /// 
        /// Makes sure the X, Y and Z values are the same.
        /// </summary>
        /// <param name="a">The first value being compared.</param>
        /// <param name="b">The second value being compared.</param>
        /// <returns>True if the two values are equal.</returns>
        public static bool operator==(Data3D a, Data3D b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        /// <summary>
        /// Overload for not equals. Required because equals is overloaded.
        /// 
        /// Makes sure that at least one of the X, Y and Z values are different.
        /// </summary>
        /// <param name="a">The first value being compared.</param>
        /// <param name="b">The second value being compared.</param>
        /// <returns>True if the two values are not equal.</returns>
        public static bool operator !=(Data3D a, Data3D b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj is Data3D p)
            {
                return p.X == X && p.Y == Y && p.Z == Z;
            }
            return base.Equals(obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        /// <summary>
        /// Assuming this is a vector, get the manhattan distance it represents.
        /// </summary>
        /// <returns></returns>
        public long ManhattanDistance()
        {
            return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }
    }
}
