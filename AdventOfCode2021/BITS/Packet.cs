using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS
{
    public class Packet
    {
        /// <summary>
        /// The version for this packet.
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// The type id for this packet.
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// The sub packets of this packet.
        /// </summary>
        public Packet[] SubPackets { get; set; }
        /// <summary>
        /// The literal value for this packet.
        /// </summary>
        public long Literal { get; set; }

        /// <summary>
        /// Create a packet.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="typeId">The type id.</param>
        public Packet(int version, int typeId)
        {
            Version = version;
            TypeId = typeId;
            SubPackets = new Packet[0];
            Literal = 0;
        }

        /// <summary>
        /// Sum up the version numbers for this packet and for all sub packets.
        /// </summary>
        /// <returns>The total of the version numbers.</returns>
        public int SumVersions()
        {
            int sum = Version;

            foreach (var subpacket in SubPackets)
            {
                sum += subpacket.SumVersions();
            }
            return sum;
        }

        /// <summary>
        /// Get the value of this packet.
        /// </summary>
        /// <returns>The total value of the packet.</returns>
        public long GetValue()
        {
            switch (TypeId)
            {
                case 0:
                    // Sum
                    return SubPackets.Sum(p => p.GetValue());
                case 1:
                    // Product
                    return SubPackets.Aggregate((long)1, (agg, packet) => agg *= packet.GetValue());
                case 2:
                    // Min
                    return SubPackets.Min(p => p.GetValue());
                case 3:
                    // Max
                    return SubPackets.Max(p => p.GetValue());
                case 4:
                    // Literal
                    return Literal;
                case 5:
                    // Greater
                    return SubPackets[0].GetValue() > SubPackets[1].GetValue() ? 1 : 0;
                case 6:
                    // Less
                    return SubPackets[0].GetValue() < SubPackets[1].GetValue() ? 1 : 0;
                case 7:
                    // Equal
                    return SubPackets[0].GetValue() == SubPackets[1].GetValue() ? 1 : 0;
            }
            return 0;
        }
    }
}
