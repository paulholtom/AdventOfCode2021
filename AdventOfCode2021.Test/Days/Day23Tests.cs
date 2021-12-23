using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode2021.Days.Day23;
using System.Collections.Generic;

namespace AdventOfCode2021.Test.Days
{
    [TestClass]
    public class Day23Tests
    {
        /// <summary>
        /// The test input.
        /// </summary>
        const string TEST_DATA = @"#############
#...........#
###B#C#B#D###
  #A#D#C#A#
  #########";

        const string SIMPLE_TEST = @"#############
#...........#
###B#A#C#D###
  #A#B#C#D#
  #########";

        [TestMethod]
        public void MoveIntoRoom()
        {
            var hallway = new char[11];
            for(int i = 0; i < 11; i++)
            {
                hallway[i] = '.';
            }
            hallway[1] = 'A';
            var rooms = new List<char>[]
            {
                new List<char>{ 'A' },
                new List<char>{ 'B', 'B' },
                new List<char>{ 'C', 'C' },
                new List<char>{ 'D', 'D' },
            };
            State state = new State(hallway, rooms, 2);

            var length = state.PathLength(1, 0);

            Assert.AreEqual(2, length);
        }

        [TestMethod]
        [DataRow(TEST_DATA, 12521)]
        [DataRow(SIMPLE_TEST, 46)]
        public void Part1(string input, int expected)
        {
            // Arrange
            var sol = new Solution(input);

            // Act
            var result = sol.RunPart1();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Part2()
        {
            // Arrange
            var sol = new Solution(TEST_DATA);

            // Act
            var result = sol.RunPart2();

            // Assert
            Assert.AreEqual(44169, result);
        }
    }
}