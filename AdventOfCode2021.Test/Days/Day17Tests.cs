using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day17;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day17Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"target area: x=20..30, y=-10..-5";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(45, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(112, result);
        }
    }
}