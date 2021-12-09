using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day5;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day5Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(12, result);
        }
    }
}