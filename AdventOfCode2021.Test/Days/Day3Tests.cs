using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day3;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day3Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(198, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(230, result);
        }
    }
}