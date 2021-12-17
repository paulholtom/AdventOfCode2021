﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS.PacketTypes
{
    /// <summary>
    /// A less than packet.
    /// </summary>
    public class Less : OperatorPacket
    {
        /// <inheritdoc/>
        public Less(int version, int typeId, Packet[] subPackets) : base(version, typeId, subPackets)
        {
            if (subPackets.Length != 2) throw new ArgumentException("Less requires exactly two sub packets", "subPackets");
        }

        /// <summary>
        /// Get the value of this packet.
        /// </summary>
        /// <returns>The value.</returns>
        public override long GetValue()
        {
            return SubPackets[0].GetValue() < SubPackets[1].GetValue() ? 1 : 0;
        }
    }
}
