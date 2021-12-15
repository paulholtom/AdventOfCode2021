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
        const string TEST_DATA = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(40, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(315, result);
        }
    }
}