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
    public class Grid2dTests
    {
        /// <summary>
        /// Test grid.
        /// </summary>
        protected Grid2d _grid;

        [TestInitialize]
        public void TestStartup()
        {
            _grid = new();
        }

        /// <summary>
        /// Check that grid values are equal.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        protected void AssertGridsEqual(List<List<int>> expected, List<List<int>> actual)
        {
            Assert.AreEqual(expected.Count(), _grid.Points.Count());

            for (int i = 0; i < expected.Count(); i++)
            {
                CollectionAssert.AreEqual(expected[i], _grid.Points[i]);
            }
        }

        [TestMethod]
        public void DrawLine_SingleLine_PointsCounted()
        {
            // Arrange
            var line = new Line2d(new Point(0, 0), new Point(0, 2));

            // Act
            _grid.DrawLine(line);

            // Assert
            var expected = new List<List<int>>
            {
                new List<int> { 1, 1, 1 },
            };

            Assert.AreEqual(0, _grid.CountOverlaps());
            AssertGridsEqual(expected, _grid.Points);
        }

        [TestMethod]
        public void DrawLine_MultipleLines_PointsCounted()
        {
            // Arrange
            var lineA = new Line2d(new Point(0, 0), new Point(0, 2));
            var lineB = new Line2d(new Point(0, 3), new Point(1, 2));

            // Act
            _grid.DrawLine(lineA);
            _grid.DrawLine(lineB);

            // Assert
            var expected = new List<List<int>>
            {
                new List<int> { 1, 1, 1, 1 },
                new List<int> { 0, 0, 1 }
            };

            Assert.AreEqual(0, _grid.CountOverlaps());
            AssertGridsEqual(expected, _grid.Points);
        }

        [TestMethod]
        public void DrawLine_OverlappingLines_PointsCounted()
        {
            // Arrange
            var lineA = new Line2d(new Point(1, 0), new Point(1, 2));
            var lineB = new Line2d(new Point(0, 1), new Point(2, 1));

            // Act
            _grid.DrawLine(lineA);
            _grid.DrawLine(lineB);

            // Assert
            var expected = new List<List<int>>
            {
                new List<int> { 0, 1 },
                new List<int> { 1, 2, 1 },
                new List<int> { 0, 1 },
            };

            Assert.AreEqual(1, _grid.CountOverlaps());
            AssertGridsEqual(expected, _grid.Points);
        }
    }
}
