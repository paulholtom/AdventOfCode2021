using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day25;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day25Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(58, result);
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