using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.BITS
{
    public  class PacketParser
    {
        /// <summary>
        /// The input as binary.
        /// </summary>
        protected string BitString { get; }

        /// <summary>
        /// Constructor for the parser.
        /// </summary>
        /// <param name="input">The hex string input.</param>
        public PacketParser(string input)
        {
            StringBuilder bits = new StringBuilder();

            foreach (var c in input)
            {
                bits.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));
            }

            BitString = bits.ToString();
        }

        /// <summary>
        /// Parse a single packet.
        /// </summary>
        /// <returns>The packet.</returns>
        public Packet ParseSingle()
        {
            var (packets, _) = ParsePacketsInString(BitString, 1);
            return packets[0];
        }

        /// <summary>
        /// Parse the packets in the provided string.
        /// </summary>
        /// <param name="bitString">The string to parse.</param>
        /// <param name="numPackets">The number of packets to stop at. Anything below 0 means parse until the end of the string.</param>
        /// <returns>The parsed packets and how much of the string was read to parse those packets.</returns>
        private (Packet[], int) ParsePacketsInString(string bitString, int numPackets = -1)
        {
            int position = 0;
            List<Packet> packets = new List<Packet>();
            while (position < bitString.Length - 6 && (numPackets < 0 || packets.Count < numPackets))
            {
                var version = Convert.ToInt32(bitString.Substring(position, 3), 2);
                position += 3;
                var typeId = Convert.ToInt32(bitString.Substring(position, 3), 2);
                position += 3;

                var packet = new Packet(version, typeId);
                packets.Add(packet);

                // Move to the next packet.
                switch (typeId)
                {
                    case 4:
                        // A literal. Figure out the number.

                        // Get the parts of the binary number until one of the chunks starts with 0.
                        StringBuilder number = new();
                        while (bitString[position] != '0')
                        {
                            // Add the next 4 bits to the number string.
                            number.Append(bitString.Substring(position + 1, 4));
                            position += 5;
                        }

                        // Get the last part.
                        number.Append(bitString.Substring(position + 1, 4));
                        position += 5;
                        packet.Literal = Convert.ToInt64(number.ToString(), 2);

                        // From here we can determine the literal number.
                        break;
                    default:
                        // An operator of some sort.
                        var mode = bitString[position];
                        position++;
                        if (mode == '0')
                        {
                            var length = Convert.ToInt32(bitString.Substring(position, 15), 2);
                            position += 15;

                            // We can parse all of the packets in the string
                            var (subPackets, _) = ParsePacketsInString(bitString.Substring(position, length));
                            packet.SubPackets = subPackets;

                            position += length;
                        }
                        else
                        {
                            var subPackets = Convert.ToInt32(bitString.Substring(position, 11), 2);
                            position += 11;

                            // Parse until we've found the appropriate number of packets.
                            var (subPackets2, distance) = ParsePacketsInString(bitString.Substring(position), subPackets);
                            packet.SubPackets = subPackets2;
                            position += distance;

                        }
                        break;
                }
            }

            return (packets.ToArray(), position);
        }
    }
}
