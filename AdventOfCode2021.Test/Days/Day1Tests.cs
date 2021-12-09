using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day1;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day1Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"199
200
208
210
200
207
240
269
260
263";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(5, result);
        }
    }
}