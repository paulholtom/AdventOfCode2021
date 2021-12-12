using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day12;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day12Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(36, result);
        }
    }
}