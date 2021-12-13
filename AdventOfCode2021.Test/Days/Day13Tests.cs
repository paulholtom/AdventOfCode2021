using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day13;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day13Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(17, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(@"#####
#...#
#...#
#...#
#####
.....
.....
", result);
        }
    }
}