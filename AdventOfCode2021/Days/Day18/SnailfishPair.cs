using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day18
{
    public class SnailfishPair : SnailfishNumberPart
    {
        /// <summary>
        /// The left part.
        /// </summary>
        public SnailfishNumberPart Left { get; set; }
        /// <summary>
        /// The right part.
        /// </summary>
        public SnailfishNumberPart Right { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="left">The left part.</param>
        /// <param name="right">The right part.</param>
        public SnailfishPair(SnailfishNumberPart left, SnailfishNumberPart right)
        {
            Left = left;
            Right = right;
            Left.Parent = this;
            Right.Parent = this;
        }

        /// <inheritdoc/>
        public override SnailfishRegular GetRightmostNumber()
        {
            return Right.GetRightmostNumber();
        }

        /// <inheritdoc/>
        public override SnailfishRegular GetLeftmostNumber()
        {
            return Left.GetLeftmostNumber();
        }

        /// <summary>
        /// Explode this pair, or one of its children, as appropriate.
        /// </summary>
        /// <param name="depth">The current depth for this pair.</param>
        /// <returns>If anything was exploded.</returns>
        public override bool Explode(int depth)
        {
            if (depth < 4)
            {
                return Left.Explode(depth + 1) || Right.Explode(depth + 1);
            }

            // Find a number to the left.
            var parent = Parent;
            var current = this;
            while(parent != null && parent.Left == current)
            {
                current = parent;
                parent = parent.Parent;
            }
            if(parent != null)
            {
                // Found a pair where this is on the right.
                // Get the rightmost number in the left part of this and add the left number to that.
                parent.Left.GetRightmostNumber().Increase(Left.GetMagnitude());
            }

            // Find a number to the right.
            parent = Parent;
            current = this;
            while (parent != null && parent.Right == current)
            {
                current = parent;
                parent = parent.Parent;
            }
            if (parent != null)
            {
                // Found a pair where this is on the left.
                // Get the leftmost number in the right part of this and add the right number to that.
                parent.Right.GetLeftmostNumber().Increase(Right.GetMagnitude());
            }

            // Replace this in the parent with a 0.
            var replacement = new SnailfishRegular(0);
            replacement.Parent = Parent;
            if (Parent?.Left == this)
                Parent.Left = replacement;
            else if (Parent?.Right == this)
                Parent.Right = replacement;

            return true;
        }

        /// <inheritdoc/>
        public override bool Split()
        {
            return Left.Split() || Right.Split();
        }

        /// <summary>
        /// Reduce this pair.
        /// </summary>
        protected void Reduce()
        {
            while (true)
            {
                if (Left.Explode(1))
                    continue;
                else if (Right.Explode(1))
                    continue;
                else if (Left.Split())
                    continue;
                else if (Right.Split())
                    continue;
                break;
            }
        }

        /// <summary>
        /// Add two snailfish numbers.
        /// </summary>
        /// <param name="left">The left number.</param>
        /// <param name="right">The right number.</param>
        /// <returns>The result of adding the two numbers.</returns>
        public static SnailfishPair operator+ (SnailfishPair left, SnailfishPair right)
        {
            var pair = new SnailfishPair(left.DeepCopy(), right.DeepCopy());
            pair.Reduce();
            return pair;
        }

        /// <inheritdoc/>
        public override int GetMagnitude()
        {
            return Left.GetMagnitude() * 3 + Right.GetMagnitude() * 2;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"[{Left},{Right}]";
        }

        /// <inheritdoc/>
        public override SnailfishNumberPart DeepCopy()
        {
            var left = Left.DeepCopy();
            var right = Right.DeepCopy();
            return new SnailfishPair(left, right);
        }
    }
}
