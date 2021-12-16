using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day16;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day16Tests
    {
        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution("8A004A801A8002F478");

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(16, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution("9C0141080250320F1802104A08");

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}