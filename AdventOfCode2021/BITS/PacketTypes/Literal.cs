using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS.PacketTypes
{
    /// <summary>
    /// A packet for a literal.
    /// </summary>
    public class Literal : Packet
    {
        /// <summary>
        /// The value of the literal.
        /// </summary>
        protected long Value { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="version">The verison of this packet.</param>
        /// <param name="typeId">The type id for this packet.</param>
        /// <param name="value">The value of the literal.</param>
        public Literal(int version, int typeId, long value) : base(version, typeId)
        {
            Value = value;
        }

        /// <inheritdoc/>
        public override long GetValue()
        {
            return Value;
        }
    }
}
