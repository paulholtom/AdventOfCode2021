using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS.PacketTypes
{
    /// <summary>
    /// A sum packet.
    /// </summary>
    public class Sum : OperatorPacket
    {
        /// <inheritdoc/>
        public Sum(int version, int typeId, Packet[] subPackets) : base(version, typeId, subPackets) { }

        /// <summary>
        /// Get the value of this packet.
        /// </summary>
        /// <returns>The value.</returns>
        public override long GetValue()
        {
            return SubPackets.Sum(p => p.GetValue());
        }
    }
}
