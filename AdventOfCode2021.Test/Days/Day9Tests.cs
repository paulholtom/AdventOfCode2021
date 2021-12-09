using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day9;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day9Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"2199943210
3987894921
9856789892
8767896789
9899965678";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(15, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(1134, result);
        }
    }
}