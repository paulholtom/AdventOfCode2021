using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2021.Coordinates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode2021.Test.Coordinates
{
    [TestClass]
    public class Line2dTests
    {
        [TestMethod]
        public void Constructor_SinglePoint()
        {
            // Arrange/Act
            var line = new Line2d($"1{Line2d.POINT_SEPARATOR}1{Line2d.LINE_SEPARATOR}1{Line2d.POINT_SEPARATOR}1");

            // Assert
            Assert.IsTrue(line.IsHorizontal);
            Assert.IsTrue(line.IsVertical);
            CollectionAssert.AreEqual(new Point[] { new Point(1, 1) }, line.Points);
        }

        [TestMethod]
        public void Constructor_Right()
        {
            // Arrange/Act
            var line = new Line2d(new Point(1, 1), new Point(3, 1));

            // Assert
            Assert.IsTrue(line.IsHorizontal);
            CollectionAssert.AreEqual(
                new Point[] { 
                    new Point(1, 1),
                    new Point(2, 1),
                    new Point(3, 1),
                }, 
                line.Points);
        }

        [TestMethod]
        public void Constructor_Left()
        {
            // Arrange/Act
            var line = new Line2d(new Point(3, 1), new Point(1, 1));

            // Assert
            Assert.IsTrue(line.IsHorizontal);
            CollectionAssert.AreEqual(
                new Point[] {
                    new Point(3, 1),
                    new Point(2, 1),
                    new Point(1, 1),
                },
                line.Points);
        }

        [TestMethod]
        public void Constructor_Down()
        {
            // Arrange/Act
            var line = new Line2d(new Point(1, 1), new Point(1, 3));

            // Assert
            Assert.IsTrue(line.IsVertical);
            CollectionAssert.AreEqual(
                new Point[] {
                    new Point(1, 1),
                    new Point(1, 2),
                    new Point(1, 3),
                },
                line.Points);
        }

        [TestMethod]
        public void Constructor_Up()
        {
            // Arrange/Act
            var line = new Line2d(new Point(1, 3), new Point(1, 1));

            // Assert
            Assert.IsTrue(line.IsVertical);
            CollectionAssert.AreEqual(
                new Point[] {
                    new Point(1, 3),
                    new Point(1, 2),
                    new Point(1, 1),
                },
                line.Points);
        }

        [TestMethod]
        public void Constructor_DownRight45()
        {
            // Arrange/Act
            var line = new Line2d(new Point(1, 1), new Point(3, 3));

            // Assert
            CollectionAssert.AreEqual(
                new Point[] {
                    new Point(1, 1),
                    new Point(2, 2),
                    new Point(3, 3),
                },
                line.Points);
        }

        [TestMethod]
        public void Constructor_UpRight45()
        {
            // Arrange/Act
            var line = new Line2d(new Point(3, 3), new Point(1, 1));

            // Assert
            CollectionAssert.AreEqual(
                new Point[] {
                    new Point(3, 3),
                    new Point(2, 2),
                    new Point(1, 1),
                },
                line.Points);
        }

        [TestMethod]
        public void Constructor_DownLeft45()
        {
            // Arrange/Act
            var line = new Line2d(new Point(1, 3), new Point(3, 1));

            // Assert
            CollectionAssert.AreEqual(
                new Point[] {
                    new Point(1, 3),
                    new Point(2, 2),
                    new Point(3, 1),
                },
                line.Points);
        }

        [TestMethod]
        public void Constructor_UpLeft45()
        {
            // Arrange/Act
            var line = new Line2d(new Point(3, 1), new Point(1, 3));

            // Assert
            CollectionAssert.AreEqual(
                new Point[] {
                    new Point(3, 1),
                    new Point(2, 2),
                    new Point(1, 3),
                },
                line.Points);
        }
    }
}
