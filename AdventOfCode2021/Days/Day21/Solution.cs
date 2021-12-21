using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day21
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 21;

        /// <summary>
        /// The starting positions for both players.
        /// </summary>
        protected int[] StartingPositions;
        /// <summary>
        /// Cached win counts for part 2.
        /// </summary>
        public Dictionary<string, long[]> CachedWinCounts { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var lines = Input.SplitLines();
            StartingPositions = new int[lines.Length];
            for (int i = 0; i < lines.Length; ++ i)
            {
                StartingPositions[i] = int.Parse(lines[i].Split(':')[1].Trim()) - 1;
            }
            CachedWinCounts = new();
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            int turn = 0;
            int roll = 0;
            var positions = new int[StartingPositions.Length];
            var scores = new int[StartingPositions.Length];
            StartingPositions.CopyTo(positions, 0);
            while(scores[0] < 1000 && scores[1] < 1000)
            {
                var player = turn % 2;
                var add = 0;
                for(int i = 0; i < 3; ++i)
                {
                    add += roll % 100 + 1;
                    roll++;
                }
                positions[player] = (positions[player] + add) % 10;
                scores[player] += positions[player] + 1;
                ++turn;
            }
            return roll * scores.Min();
        }

        /// <summary>
        /// Handle the results of all splits from a single turn.
        /// </summary>
        /// <param name="player">The player who's turn it is.</param>
        /// <param name="rollTotal">The total of the rolls that led to this split.</param>
        /// <param name="scores">The current scores of the players.</param>
        /// <param name="positions">The current positions of the players.</param>
        /// <returns>The number of branches from here that result in each player winning.</returns>
        public long[] UniverseSplit(int player, int rollTotal, int[] scores, int[] positions)
        {
            string key = $"{player}_{rollTotal}_{scores[0]}_{scores[1]}_{positions[0]}_{positions[1]}";
            if (CachedWinCounts.ContainsKey(key)) 
                return CachedWinCounts[key];
            positions[player] = (positions[player] + rollTotal) % 10;
            scores[player] += positions[player] + 1;

            var returnValue = new long[2];
            if (scores[player] >= 21)
            {
                returnValue[player] = 1;
            }
            else
            {
                var nextPlayer = (player + 1) % 2;

                for (var roll1 = 1; roll1 <= 3; ++roll1)
                {
                    for (var roll2 = 1; roll2 <= 3; ++roll2)
                    {
                        for (var roll3 = 1; roll3 <= 3; ++roll3)
                        {
                            var val = UniverseSplit(nextPlayer, roll1 + roll2 + roll3, (int[])scores.Clone(), (int[])positions.Clone());
                            returnValue[0] += val[0];
                            returnValue[1] += val[1];
                        }
                    }
                }
            }

            CachedWinCounts[key] = returnValue;
            return returnValue;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            var positions = new int[StartingPositions.Length];
            var scores = new int[StartingPositions.Length];
            StartingPositions.CopyTo(positions, 0);
            var winCounts = new long[StartingPositions.Length];
            for (var roll1 = 1; roll1 <= 3; ++roll1)
            {
                for (var roll2 = 1; roll2 <= 3; ++roll2)
                {
                    for (var roll3 = 1; roll3 <= 3; ++roll3)
                    {
                        var val = UniverseSplit(0, roll1 + roll2 + roll3, (int[])scores.Clone(), (int[])positions.Clone());
                        winCounts[0] += val[0];
                        winCounts[1] += val[1];
                    }
                }
            }
            return winCounts.Max();
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
