using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Test
{
    [TestClass]
    public class SubmarineTests
    {
        /// <summary>
        /// The submarine.
        /// </summary>
        private Submarine _sub = new();

        [TestInitialize]
        public void TestInit()
        {
            _sub = new();
        }

        [TestMethod]
        public void Up_DecreaseAim()
        {
            // Arrange
            int magnitude = 5;

            // Act
            _sub.RunInstructions(new string[]{$"{Submarine.UP} {magnitude}"});

            // Assert
            Assert.AreEqual(-magnitude, _sub.Aim);
        }

        [TestMethod]
        public void Down_IncreaseAim()
        {
            // Arrange
            int magnitude = 5;

            // Act
            _sub.RunInstructions($"{Submarine.DOWN} {magnitude}");

            // Assert
            Assert.AreEqual(magnitude, _sub.Aim);
        }

        [TestMethod]
        public void Forward_IncreaseHorizontal()
        {
            // Arrange
            int magnitude = 5;

            // Act
            _sub.RunInstructions($"{Submarine.FORWARD} {magnitude}");

            // Assert
            Assert.AreEqual(magnitude, _sub.HorizontalPosition);
        }

        [TestMethod]
        public void DownThenForward_IncreaseHorizontalAndDepth()
        {
            // Arrange
            int aim = 5;
            int distance = 3;

            // Act
            _sub.RunInstructions( 
                $"{Submarine.DOWN} {aim}",
                $"{Submarine.FORWARD} {distance}" 
            );

            // Assert
            Assert.AreEqual(distance, _sub.HorizontalPosition);
            Assert.AreEqual(aim * distance, _sub.Depth);
        }
    }
}
