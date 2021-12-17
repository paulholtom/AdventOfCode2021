using AdventOfCode2021.BITS.PacketTypes;
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
        /// The number of bits in the version.
        /// </summary>
        private const int VERSION_SIZE = 3;
        /// <summary>
        /// The number of bits in the type id.
        /// </summary>
        private const int TYPE_ID_SIZE = 3;
        /// <summary>
        /// The type id for a sum packet.
        /// </summary>
        private const int SUM = 0;
        /// <summary>
        /// The type id for a product packet.
        /// </summary>
        private const int PRODUCT = 1;
        /// <summary>
        /// The type id for a minimum packet.
        /// </summary>
        private const int MINIMUM = 2;
        /// <summary>
        /// The type id for a maximum packet.
        /// </summary>
        private const int MAXIMUM = 3;
        /// <summary>
        /// The type id for a literal packet.
        /// </summary>
        private const int LITERAL = 4;
        /// <summary>
        /// The type id for a greater than packet.
        /// </summary>
        private const int GREATER = 5;
        /// <summary>
        /// The type id for a less than packet.
        /// </summary>
        private const int LESS = 6;
        /// <summary>
        /// The type id for an equality packet.
        /// </summary>
        private const int EQUAL = 7;
        /// <summary>
        /// The number of bits for the length of the sub packets in bit mode.
        /// </summary>
        private const int LENGTH_MODE_BIT = 15;
        /// <summary>
        /// The number of bits for the number of packets in packet mode.
        /// </summary>
        private const int LENGTH_MODE_PACKET = 11;

        /// <summary>
        /// The input as binary.
        /// </summary>
        protected string BitString { get; }
        /// <summary>
        /// The current position in the bit string.
        /// </summary>
        protected int Position { get; set; }

        /// <summary>
        /// Constructor for the parser.
        /// </summary>
        /// <param name="input">The hex string input.</param>
        public PacketParser(string input)
        {
            StringBuilder bits = new StringBuilder();

            // Convert each hex character into 4 bits.
            foreach (var c in input)
            {
                bits.Append(Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));
            }

            Position = 0;
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
                var version = Convert.ToInt32(bitString.Substring(position, VERSION_SIZE), 2);
                position += VERSION_SIZE;
                var typeId = Convert.ToInt32(bitString.Substring(position, TYPE_ID_SIZE), 2);
                position += TYPE_ID_SIZE;

                if(typeId == LITERAL)
                {
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

                    // Create the literal packet.
                    packets.Add(new Literal(version, typeId, Convert.ToInt64(number.ToString(), 2)));
                }
                else
                {
                    // An operator of some sort.
                    var mode = bitString[position];
                    position++;
                    Packet[] subPackets;
                    if (mode == '0')
                    {
                        var length = Convert.ToInt32(bitString.Substring(position, LENGTH_MODE_BIT), 2);
                        position += LENGTH_MODE_BIT;

                        // We can parse all of the packets in the string
                        (subPackets, var _) = ParsePacketsInString(bitString.Substring(position, length));

                        position += length;
                    }
                    else
                    {
                        var packetCount = Convert.ToInt32(bitString.Substring(position, LENGTH_MODE_PACKET), 2);
                        position += LENGTH_MODE_PACKET;

                        // Parse until we've found the appropriate number of packets.
                        (subPackets, var distance) = ParsePacketsInString(bitString.Substring(position), packetCount);
                        position += distance;
                    }

                    switch (typeId)
                    {
                        case SUM:
                            packets.Add(new Sum(version, typeId, subPackets));
                            break;
                        case PRODUCT:
                            packets.Add(new Product(version, typeId, subPackets));
                            break;
                        case MINIMUM:
                            packets.Add(new Minimum(version, typeId, subPackets));
                            break;
                        case MAXIMUM:
                            packets.Add(new Maximum(version, typeId, subPackets));
                            break;
                        case GREATER:
                            packets.Add(new Greater(version, typeId, subPackets));
                            break;
                        case LESS:
                            packets.Add(new Less(version, typeId, subPackets));
                            break;
                        case EQUAL:
                            packets.Add(new Equal(version, typeId, subPackets));
                            break;
                    }
                }
            }

            return (packets.ToArray(), position);
        }
    }
}
