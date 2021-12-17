using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS.PacketTypes
{
    /// <summary>
    /// A min packet.
    /// </summary>
    public class Minimum : OperatorPacket
    {
        /// <inheritdoc/>
        public Minimum(int version, int typeId, Packet[] subPackets) : base(version, typeId, subPackets) { }

        /// <summary>
        /// Get the value of this packet.
        /// </summary>
        /// <returns>The value.</returns>
        public override long GetValue()
        {
            return SubPackets.Min(p => p.GetValue());
        }
    }
}
