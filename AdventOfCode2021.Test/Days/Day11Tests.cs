using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day11;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day11Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(1656, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(195, result);
        }
    }
}