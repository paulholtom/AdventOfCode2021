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
            var version = Convert.ToInt32(BitString.Substring(Position, VERSION_SIZE), 2);
            Position += VERSION_SIZE;
            var typeId = Convert.ToInt32(BitString.Substring(Position, TYPE_ID_SIZE), 2);
            Position += TYPE_ID_SIZE;

            if (typeId == LITERAL)
            {
                // Get the parts of the binary number until one of the chunks starts with 0.
                StringBuilder number = new();
                while (BitString[Position] != '0')
                {
                    // Add the next 4 bits to the number string.
                    number.Append(BitString.Substring(Position + 1, 4));
                    Position += 5;
                }

                // Get the last part.
                number.Append(BitString.Substring(Position + 1, 4));
                Position += 5;

                // Create the literal packet.
                return new Literal(version, typeId, Convert.ToInt64(number.ToString(), 2));
            }
            else
            {
                // An operator of some sort.
                var mode = BitString[Position];
                Position++;
                var subPackets = new List<Packet>();
                if (mode == '0')
                {
                    var length = Convert.ToInt32(BitString.Substring(Position, LENGTH_MODE_BIT), 2);
                    Position += LENGTH_MODE_BIT;

                    var end = Position + length;

                    // We can parse all of the packets in the string
                    while (Position < end)
                        subPackets.Add(ParseSingle());
                }
                else
                {
                    var packetCount = Convert.ToInt32(BitString.Substring(Position, LENGTH_MODE_PACKET), 2);
                    Position += LENGTH_MODE_PACKET;

                    // Parse until we've found the appropriate number of packets.
                    for (int i = 0; i < packetCount; i++)
                    {
                        subPackets.Add(ParseSingle());
                    }
                }

                switch (typeId)
                {
                    case SUM:
                        return new Sum(version, typeId, subPackets.ToArray());
                    case PRODUCT:
                        return new Product(version, typeId, subPackets.ToArray());
                    case MINIMUM:
                        return new Minimum(version, typeId, subPackets.ToArray());
                    case MAXIMUM:
                        return new Maximum(version, typeId, subPackets.ToArray());
                    case GREATER:
                        return new Greater(version, typeId, subPackets.ToArray());
                    case LESS:
                        return new Less(version, typeId, subPackets.ToArray());
                    case EQUAL:
                    default:
                        return new Equal(version, typeId, subPackets.ToArray());
                }
            }
        }
    }
}
