using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day14;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day14Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

        [TestMethod]
        public void Part1()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(1588, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(2188189693529, result);
        }
    }
}