using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2021.BITS;
using AdventOfCode2021.BITS.PacketTypes;

namespace AdventOfCode2021.Test.BITS
{
    [TestClass]
    public class PacketParserTests
    {
        [TestMethod]
        public void SingleLiteralPacket()
        {
            // Arrange
            var parser = new PacketParser("D2FE28");

            // Act
            var packet = parser.ParseSingle();

            // Assert
            Assert.IsNotNull(packet);
            Assert.AreEqual(6, packet.Version);
            Assert.AreEqual(4, packet.TypeId);
            Assert.AreEqual(2021, packet.GetValue());
        }

        [TestMethod]
        public void SingleOperaterPacket_LengthType0()
        {
            // Arrange
            var parser = new PacketParser("38006F45291200");

            // Act
            var packet = parser.ParseSingle();

            // Assert
            Assert.IsNotNull(packet);
            Assert.AreEqual(1, packet.Version);
            Assert.AreEqual(6, packet.TypeId);
            Assert.IsInstanceOfType(packet, typeof(Less));
            var less = (Less)packet;
            Assert.AreEqual(2, less.SubPackets.Length);
            Assert.AreEqual(10, less.SubPackets[0].GetValue());
            Assert.AreEqual(20, less.SubPackets[1].GetValue());
        }

        [TestMethod]
        public void SingleOperaterPacket_LengthType1()
        {
            // Arrange
            var parser = new PacketParser("EE00D40C823060");

            // Act
            var packet = parser.ParseSingle();

            // Assert
            Assert.IsNotNull(packet);
            Assert.AreEqual(7, packet.Version);
            Assert.AreEqual(3, packet.TypeId);
            Assert.IsInstanceOfType(packet, typeof(Maximum));
            var max = (Maximum)packet;
            Assert.AreEqual(3, max.SubPackets.Length);
            Assert.AreEqual(1, max.SubPackets[0].GetValue());
            Assert.AreEqual(2, max.SubPackets[1].GetValue());
            Assert.AreEqual(3, max.SubPackets[2].GetValue());
        }

        [TestMethod]
        [DataRow("8A004A801A8002F478", 16)]
        [DataRow("620080001611562C8802118E34", 12)]
        [DataRow("C0015000016115A2E0802F182340", 23)]
        [DataRow("A0016C880162017C3686B18A3D4780", 31)]
        public void SumVersions(string input, int expected)
        {
            // Arrange
            var parser = new PacketParser(input);

            // Act
            var packet = parser.ParseSingle();

            // Assert
            Assert.IsNotNull(packet);
            Assert.AreEqual(expected, packet.SumVersions());
        }

        [TestMethod]
        [DataRow("C200B40A82", 3)] // 1 + 2
        [DataRow("04005AC33890", 54)] // 6 * 9
        [DataRow("880086C3E88112", 7)] // Min(7, 8, 9)
        [DataRow("CE00C43D881120", 9)] // Max(7, 8, 9)
        [DataRow("D8005AC2A8F0", 1)] // 5 < 15
        [DataRow("F600BC2D8F", 0)] // 5 > 15
        [DataRow("9C005AC2F8F0", 0)] // 5 == 15
        [DataRow("9C0141080250320F1802104A08", 1)] // (1 + 3) == (2 * 2)
        public void Operators(string input, long expected)
        {
            // Arrange
            var parser = new PacketParser(input);

            // Act
            var packet = parser.ParseSingle();

            // Assert
            Assert.IsNotNull(packet);
            Assert.AreEqual(expected, packet.GetValue());
        }
    }
}
