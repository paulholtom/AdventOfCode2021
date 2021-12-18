using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day18
{
    /// <summary>
    /// A left or right part of a snailfish number.
    /// </summary>
    public abstract class SnailfishNumberPart
    {
        /// <summary>
        /// The immediate parent of this part.
        /// </summary>
        public SnailfishPair? Parent { get; set; }

        /// <summary>
        /// Determine if this or any child needs to explode, explode it if necessary.
        /// </summary>
        /// <param name="depth">How deep this is.</param>
        /// <returns>True if exploded, false otherwise.</returns>
        public abstract bool Explode(int depth);

        /// <summary>
        /// Determine if this or any child needs to split. Split if necessary.
        /// </summary>
        /// <returns>If anything split</returns>
        public abstract bool Split();

        /// <summary>
        /// Get the rightmost number.
        /// </summary>
        /// <returns>Returns the rightmost regular number.</returns>
        public abstract SnailfishRegular GetRightmostNumber();

        /// <summary>
        /// Get the leftmost number.
        /// </summary>
        /// <returns>Returns the leftmost regular number.</returns>
        public abstract SnailfishRegular GetLeftmostNumber();

        /// <summary>
        /// Get the magnitude.
        /// </summary>
        /// <returns>The magnitude.</returns>
        public abstract int GetMagnitude();

        /// <summary>
        /// Do a deep copy of this.
        /// </summary>
        /// <returns>The deep copy.</returns>
        public abstract SnailfishNumberPart DeepCopy();
    }
}
