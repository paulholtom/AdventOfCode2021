using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day14
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 14;

        /// <summary>
        /// The polymer.
        /// </summary>
        protected string Polymer { get; set; }

        /// <summary>
        /// The pair insertion rules.
        /// </summary>
        protected Dictionary<string, string> PairInsertions { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var inputLines = Input.SplitLines();

            Polymer = inputLines[0];
            PairInsertions = new Dictionary<string, string>();

            for (int i = 2; i < inputLines.Length; i++)
            {
                var insertionSplit = inputLines[i].Split(" -> ");
                PairInsertions[insertionSplit[0]] = insertionSplit[1];
            }
        }

        /// <summary>
        /// Merge two count dictionaries together.
        /// </summary>
        /// <param name="a">The first dictionary.</param>
        /// <param name="b">The second dictionary.</param>
        /// <returns>The merged dictionary.</returns>
        protected Dictionary<char, long> MergeCounts(Dictionary<char, long> a, Dictionary<char, long> b)
        {
            Dictionary<char, long> counts = new Dictionary<char, long>(a);

            foreach (var key in b.Keys)
            {
                if (!counts.ContainsKey(key)) counts.Add(key, b[key]);
                else counts[key] += b[key];
            }

            return counts;
        }

        /// <summary>
        /// Cache counts of characters that result from various pairs for various numbers of steps.
        /// </summary>
        Dictionary<string, Dictionary<int, Dictionary<char, long>>> CachedCounts = new();

        /// <summary>
        /// Get the count of characters that will occur running the provided pair for the number of steps.
        /// 
        /// This does NOT include counting the original pair.
        /// </summary>
        /// <param name="pair">The pair.</param>
        /// <param name="steps">The number of steps.</param>
        /// <returns>A dictionary of character counts that result.</returns>
        protected Dictionary<char, long> GetCounts(string pair, int steps)
        {
            if (steps == 0) return new Dictionary<char, long>();
            if (!CachedCounts.ContainsKey(pair))
                CachedCounts[pair] = new();

            if (CachedCounts[pair].ContainsKey(steps))
            {
                return CachedCounts[pair][steps];
            }
            Dictionary<char, long> counts = new();

            var addedChar = PairInsertions[pair];
            counts.Add(addedChar[0], 1);
            counts = MergeCounts(counts, MergeCounts(GetCounts("" + pair[0] + addedChar, steps - 1), GetCounts("" + addedChar + pair[1], steps - 1)));

            CachedCounts[pair].Add(steps, counts);
            return counts;
        }

        /// <summary>
        /// Run the process for the number of steps provided.
        /// </summary>
        /// <param name="steps">The number of steps.</param>
        /// <returns>The difference between the most common and least common characters.</returns>
        public long RunForSteps(int steps)
        {
            Dictionary<char, long> counts = new();

            // Count the initial characters.
            foreach (var c in Polymer)
            {
                if (!counts.ContainsKey(c)) counts.Add(c, 1);
                else counts[c]++;
            }

            for (int i = 0; i < Polymer.Length - 1; ++i)
            {
                counts = MergeCounts(counts, GetCounts(Polymer.Substring(i, 2), steps));
            }
            var orderedCounts = counts.Select(c => c.Value).OrderBy(c => c).ToArray();

            return orderedCounts[orderedCounts.Length - 1] - orderedCounts[0];
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public long RunPart1()
        {
            return RunForSteps(10);
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            return RunForSteps(40);
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
