using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS.PacketTypes
{
    /// <summary>
    /// An abstract base for all types of operator packets.
    /// </summary>
    public abstract class OperatorPacket : Packet
    {
        /// <summary>
        /// The sub packets.
        /// </summary>
        public Packet[] SubPackets { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="version">The verison of this packet.</param>
        /// <param name="typeId">The type id for this packet.</param>
        /// <param name="subPackets">The sub packets of this packet.</param>
        public OperatorPacket(int version, int typeId, Packet[] subPackets) : base(version, typeId)
        {
            SubPackets = subPackets;
        }

        /// <inheritdoc/>
        public override int SumVersions()
        {
            int sum = Version;

            foreach (var subpacket in SubPackets)
            {
                sum += subpacket.SumVersions();
            }
            return sum;
        }
    }
}
