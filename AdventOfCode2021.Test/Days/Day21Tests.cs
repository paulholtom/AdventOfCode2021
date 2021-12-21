using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day21;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day21Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"Player 1 starting position: 4
Player 2 starting position: 8";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(739785, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(444356092776315, result);
        }
    }
}