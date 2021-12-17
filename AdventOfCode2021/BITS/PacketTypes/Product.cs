using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS.PacketTypes
{
    /// <summary>
    /// A product packet.
    /// </summary>
    public class Product : OperatorPacket
    {
        /// <inheritdoc/>
        public Product(int version, int typeId, Packet[] subPackets) : base(version, typeId, subPackets) { }

        /// <summary>
        /// Get the value of this packet.
        /// </summary>
        /// <returns>The value.</returns>
        public override long GetValue()
        {
            return SubPackets.Aggregate((long)1, (agg, packet) => agg *= packet.GetValue());
        }
    }
}
