using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day2;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day2Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(150, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(900, result);
        }
    }
}