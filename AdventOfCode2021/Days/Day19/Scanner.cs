using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day19
{
    /// <summary>
    /// Information about beacons detected by a scanner.
    /// </summary>
    public class Scanner
    {
        /// <summary>
        /// The beacons this scanner can detect.
        /// </summary>
        public Data3D[] Beacons { get; }

        /// <summary>
        /// The distances between beacons.
        /// </summary>
        public Data3D[][] Distances { get; }

        /// <summary>
        /// The position of this scanner.
        /// </summary>
        public Data3D Position { get; set; }

        /// <summary>
        /// All of the possible transformations needed to get things in the same orientation.
        /// </summary>
        public static readonly Func<Data3D, Data3D>[] Transforms = {
            (point) => new Data3D(point.X, point.Y, point.Z),
            (point) => new Data3D(point.X, -point.Y, point.Z),
            (point) => new Data3D(point.X, point.Y, -point.Z),
            (point) => new Data3D(point.X, -point.Y, -point.Z),
            (point) => new Data3D(point.X, point.Z, point.Y),
            (point) => new Data3D(point.X, -point.Z, point.Y),
            (point) => new Data3D(point.X, point.Z, -point.Y),
            (point) => new Data3D(point.X, -point.Z, -point.Y),
            (point) => new Data3D(-point.X, point.Y, point.Z),
            (point) => new Data3D(-point.X, -point.Y, point.Z),
            (point) => new Data3D(-point.X, point.Y, -point.Z),
            (point) => new Data3D(-point.X, -point.Y, -point.Z),
            (point) => new Data3D(-point.X, point.Z, point.Y),
            (point) => new Data3D(-point.X, -point.Z, point.Y),
            (point) => new Data3D(-point.X, point.Z, -point.Y),
            (point) => new Data3D(-point.X, -point.Z, -point.Y),
            (point) => new Data3D(point.Y, point.X, point.Z),
            (point) => new Data3D(point.Y, -point.X, point.Z),
            (point) => new Data3D(point.Y, point.X, -point.Z),
            (point) => new Data3D(point.Y, -point.X, -point.Z),
            (point) => new Data3D(point.Y, point.Z, point.X),
            (point) => new Data3D(point.Y, -point.Z, point.X),
            (point) => new Data3D(point.Y, point.Z, -point.X),
            (point) => new Data3D(point.Y, -point.Z, -point.X),
            (point) => new Data3D(-point.Y, point.X, point.Z),
            (point) => new Data3D(-point.Y, -point.X, point.Z),
            (point) => new Data3D(-point.Y, point.X, -point.Z),
            (point) => new Data3D(-point.Y, -point.X, -point.Z),
            (point) => new Data3D(-point.Y, point.Z, point.X),
            (point) => new Data3D(-point.Y, -point.Z, point.X),
            (point) => new Data3D(-point.Y, point.Z, -point.X),
            (point) => new Data3D(-point.Y, -point.Z, -point.X),
            (point) => new Data3D(point.Z, point.X, point.Y),
            (point) => new Data3D(point.Z, -point.X, point.Y),
            (point) => new Data3D(point.Z, point.X, -point.Y),
            (point) => new Data3D(point.Z, -point.X, -point.Y),
            (point) => new Data3D(point.Z, point.Y, point.X),
            (point) => new Data3D(point.Z, -point.Y, point.X),
            (point) => new Data3D(point.Z, point.Y, -point.X),
            (point) => new Data3D(point.Z, -point.Y, -point.X),
            (point) => new Data3D(-point.Z, point.X, point.Y),
            (point) => new Data3D(-point.Z, -point.X, point.Y),
            (point) => new Data3D(-point.Z, point.X, -point.Y),
            (point) => new Data3D(-point.Z, -point.X, -point.Y),
            (point) => new Data3D(-point.Z, point.Y, point.X),
            (point) => new Data3D(-point.Z, -point.Y, point.X),
            (point) => new Data3D(-point.Z, point.Y, -point.X),
            (point) => new Data3D(-point.Z, -point.Y, -point.X),
        };

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="beacons">The beacons this scanner can detect.</param>
        public Scanner(Data3D[] beacons)
        {
            Position = new Data3D(0, 0, 0);
            Beacons = beacons;

            // Get the distances between beacons. Don't bother with the last 11 beacons as there can't be 12 overlapping beacons at that point.
            Distances = new Data3D[Beacons.Length - 11][];

            for(int i = 0; i < Distances.Length; i++)
            {
                Distances[i] = new Data3D[Beacons.Length];
                for(int j = 0; j < Distances[i].Length; j++)
                {
                    Distances[i][j] = Beacons[i] - Beacons[j];
                }
            }
        }

        /// <summary>
        /// Place this scanner appropriately on the grid and flip to the appropriate orientation.
        /// </summary>
        /// <param name="transform">The change needed to flip the orientation.</param>
        /// <param name="adjustment">The move needed after the transform is applied to get to the appropriate grid location.</param>
        private void TransformAndAdjust(Func<Data3D, Data3D> transform, Data3D adjustment)
        {
            Position = adjustment;

            // Each beacon needs to have the orientation changed then be move appropriately.
            for(int i = 0; i < Beacons.Length; i++)
            {
                Beacons[i] = transform(Beacons[i]) + adjustment;
            }

            // Distances only need the orientation change.
            for(int i = 0;i < Distances.Length; i++)
            {
                for(int j= 0; j < Distances[i].Length; j++)
                {
                    Distances[i][j] = transform(Distances[i][j]);
                }
            }
        }

        /// <summary>
        /// Checks for at least 12 overlapping beacons.
        /// 
        /// If it finds them otherScanner is adjusted to use the same coordinates as this scanner.
        /// </summary>
        /// <param name="otherScanner">The scanner to check.</param>
        /// <returns>True if at least 12 overlapping beacons were found.</returns>
        public bool OverlappingWith(Scanner otherScanner)
        {
            foreach (var transform in Transforms)
            {
                for (int i = 0; i < Distances.Length; i++)
                {
                    for (int j = 0; j < otherScanner.Distances.Length; j++)
                    {
                        var matches = otherScanner.Distances[j].Where((d) => Distances[i].Contains(transform(d)));
                        if (matches.Count() >= 12)
                        {
                            // Determine the adjustment needed to the beacons
                            var adjustment = Beacons[i] - transform(otherScanner.Beacons[j]);
                            otherScanner.TransformAndAdjust(transform, adjustment);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
