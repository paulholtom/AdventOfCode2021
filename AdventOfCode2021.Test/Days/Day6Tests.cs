using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day6;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day6Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"3,4,3,1,2";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(5934, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(26984457539, result);
        }
    }
}