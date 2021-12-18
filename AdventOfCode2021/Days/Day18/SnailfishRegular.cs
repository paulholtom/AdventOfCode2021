using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day18
{
    /// <summary>
    /// A regular number contained in a snailfish number.
    /// </summary>
    public class SnailfishRegular: SnailfishNumberPart
    {
        /// <summary>
        /// The value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value.</param>
        public SnailfishRegular(int value)
        {
            Value = value;
        }

        /// <inheritdoc/>
        public override bool Explode(int depth)
        {
            // Regular numbers never explode.
            return false;
        }

        /// <inheritdoc/>
        public override bool Split()
        {
            if(Value >= 10)
            {
                var replacement = new SnailfishPair(
                    new SnailfishRegular((int)Math.Floor(Value/2f)), 
                    new SnailfishRegular((int)Math.Ceiling(Value/2f)));
                replacement.Parent = Parent;

                if(Parent?.Left == this) 
                    Parent.Left = replacement;
                else if (Parent?.Right == this)
                    Parent.Right = replacement;
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public override SnailfishRegular GetRightmostNumber()
        {
            return this;
        }

        /// <inheritdoc/>
        public override SnailfishRegular GetLeftmostNumber()
        {
            return this;
        }

        /// <inheritdoc/>
        public override int GetMagnitude()
        {
            return Value;
        }

        /// <summary>
        /// Increase the value.
        /// </summary>
        /// <param name="amount">Amount to increase it by.</param>
        public void Increase(int amount)
        {
            Value += amount;
        }

        /// <summary>
        /// Get the string representation.
        /// </summary>
        /// <returns>The value as a string.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <inheritdoc/>
        public override SnailfishNumberPart DeepCopy()
        {
            return new SnailfishRegular(Value);
        }
    }
}
