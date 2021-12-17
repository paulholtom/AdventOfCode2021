using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS
{
    /// <summary>
    /// Abstract base class for all types of packets.
    /// </summary>
    public abstract class Packet
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
        /// Create a packet.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="typeId">The type id.</param>
        public Packet(int version, int typeId)
        {
            Version = version;
            TypeId = typeId;
        }

        /// <summary>
        /// Sum up the version numbers for this packet and for all sub packets.
        /// </summary>
        /// <returns>The total of the version numbers.</returns>
        public virtual int SumVersions()
        {
            return Version;
        }

        /// <summary>
        /// Get the value of this packet.
        /// </summary>
        /// <returns>The total value of the packet.</returns>
        public abstract long GetValue();
    }
}
