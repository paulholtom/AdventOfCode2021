using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day15;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day15Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"INPUT";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}