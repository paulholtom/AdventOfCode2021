using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day24;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day24Tests
    {
        [TestMethod]
        public void Input()
        {
            const long val = 27;
            // Arrange
            var alu = new ALU(new string[] { "inp x" });
            alu.Inputs.Enqueue(val);

            // Act
            alu.Run();
            var result = alu.Variables['x'];

            // Assert
            Assert.AreEqual(val, result);
        }

        [TestMethod]
        [DataRow("add", 6, 27, 33)]
        [DataRow("mul", 3, 5, 15)]
        [DataRow("div", 10, 3, 3)]
        [DataRow("div", -10, 3, -3)]
        [DataRow("mod", 10, 3, 1)]
        [DataRow("mod", 10, 3, 1)]
        [DataRow("eql", 20, 12, 0)]
        [DataRow("eql", 6, 6, 1)]
        public void BinaryOpValue(string op, long valueA, long valueB, long expected)
        {
            // Arrange
            var alu = new ALU(new string[] { "inp x", $"{op} x {valueB}" });
            alu.Inputs.Enqueue(valueA);

            // Act
            alu.Run();
            var result = alu.Variables['x'];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("add", 6, 27, 33)]
        [DataRow("mul", 3, 5, 15)]
        [DataRow("div", 10, 3, 3)]
        [DataRow("div", -10, 3, -3)]
        [DataRow("mod", 10, 3, 1)]
        [DataRow("mod", 10, 3, 1)]
        [DataRow("eql", 20, 12, 0)]
        [DataRow("eql", 6, 6, 1)]
        public void BinaryOpVariable(string op, long valueA, long valueB, long expected)
        {
            // Arrange
            var alu = new ALU(new string[] { "inp x", "inp y", $"{op} x y" });
            alu.Inputs.Enqueue(valueA);
            alu.Inputs.Enqueue(valueB);
            alu.Run();

            // Act
            var result = alu.Variables['x'];

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}