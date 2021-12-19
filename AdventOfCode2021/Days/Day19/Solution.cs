using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day19
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 19;

        /// <summary>
        /// The positions of all of the beacons.
        /// </summary>
        protected List<Data3D> Beacons { get; }
        /// <summary>
        /// The positions of all of the scanners.
        /// </summary>
        protected List<Data3D> Scanners { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            Scanners = new();
            List<Scanner> scanners = new();

            // Parse the input.
            List<Data3D> currentScanner = new List<Data3D>();
            foreach (var line in Input.SplitLines())
            {
                if(string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith("---"))
                {
                    if (currentScanner.Count > 0)
                    {
                        scanners.Add(new Scanner(currentScanner.ToArray()));
                        currentScanner = new List<Data3D>();
                    }
                    continue;
                }

                var coords = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                currentScanner.Add(new Data3D(int.Parse(coords[0]), int.Parse(coords[1]), int.Parse(coords[2])));
            }
            // Add the last scanner in.
            scanners.Add(new Scanner(currentScanner.ToArray()));


            // Get everything relative to scanner 0. So all of the beacons from that go into the final map.
            Beacons = new();
            foreach (var beacon in scanners[0].Beacons)
            {
                Beacons.Add(beacon);
            }
            Scanners.Add(new Data3D(0, 0, 0));

            // Move scanner 0 into the list of scanners to process.
            var scannersToProcess = new List<Scanner>();
            scannersToProcess.Add(scanners[0]);
            scanners.RemoveAt(0);

            // Find scanners that overlap with scanners that have been shifted to match scanner 0.
            // Shift them and add their beacons to the beacon list.
            while(scannersToProcess.Count > 0)
            {
                var scanner = scannersToProcess[0];
                scannersToProcess.RemoveAt(0);

                int pos = 0;
                while(pos < scanners.Count)
                {
                    if (scanner.OverlappingWith(scanners[pos]))
                    {
                        foreach(var beacon in scanners[pos].Beacons)
                        {
                            if(!Beacons.Contains(beacon)) Beacons.Add(beacon);
                        }
                        Scanners.Add(scanners[pos].Position);
                        scannersToProcess.Add(scanners[pos]);
                        scanners.RemoveAt(pos);
                    }
                    else
                        ++pos;
                }
            }
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            return Beacons.Count;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            long max = 0;
            for(int i = 0; i < Scanners.Count; i++)
            {
                for (int j = i + 1; j < Scanners.Count; j++)
                {
                    var dist = (Scanners[i] - Scanners[j]).Magnitude;
                    if(dist > max) max = dist;
                }
            }
            return max;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
