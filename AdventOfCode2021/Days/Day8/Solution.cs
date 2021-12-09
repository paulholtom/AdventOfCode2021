using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day8
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 8;

        protected List<Entry> Entries { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var lines = Input.SplitLines();
            
            Entries = new List<Entry>();
            foreach(var line in lines)
            {
                Entries.Add(new Entry(line));
            }
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            var count = 0;
            var uniqueLengths = new int[] { 2, 4, 3, 7 };
            foreach (var entry in Entries)
            {
                foreach(var output in entry.Output)
                {
                    if (uniqueLengths.Contains(output.Length))
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public int RunPart2()
        {
            int total = 0;
            foreach(Entry entry in Entries)
            {
                total += entry.ParseOutput();
            }
            return total;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
