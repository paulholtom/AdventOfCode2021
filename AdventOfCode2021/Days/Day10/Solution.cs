using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day10
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 10;

        /// <summary>
        /// Information about different types of chunks.
        /// 
        /// Keys are the opening characters.
        /// </summary>
        protected Dictionary<char, ChunkInfo> ChunkSettings { get; } = new Dictionary<char, ChunkInfo>
        {
            {'(', new ChunkInfo('(', ')', 3, 1) },
            {'[', new ChunkInfo('[', ']', 57, 2) },
            {'{', new ChunkInfo('{', '}', 1197, 3) },
            {'<', new ChunkInfo('<', '>', 25137, 4) }
        };

        /// <summary>
        /// The input split into lines.
        /// </summary>
        protected string[] InputLines { get; }

        /// <summary>
        /// The score for corrupted lines.
        /// </summary>
        protected long CorruptedLinesScore { get; set;  }

        /// <summary>
        /// The scores for incomplete lines.
        /// </summary>
        protected List<long> IncompleteLineScores { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            InputLines = Input.SplitLines();

            CorruptedLinesScore = 0;
            IncompleteLineScores = new();

            foreach (var line in InputLines)
            {
                CheckLine(line);
            }
        }

        /// <summary>
        /// Check a line to determine if it's corrupted or incomplete and add to the appropriate scores.
        /// </summary>
        /// <param name="line">The line to check.</param>
        protected void CheckLine(string line)
        {
            // Track the currently open chunks in a stack.
            Stack<char> openChunks = new();
            foreach (var c in line)
            {
                // If this is an opening character, push it to the stack.
                if (ChunkSettings.ContainsKey(c))
                {
                    openChunks.Push(c);
                }
                else
                {
                    var settings = ChunkSettings.Values.Where(cs => cs.Close == c).FirstOrDefault();
                    // This is a character that closes a chunk.
                    if (settings != null)
                    {
                        // This character doesn't match the last opened chunk, this line is corrupted.
                        if (openChunks.Count == 0 || settings.Open != openChunks.Peek())
                        {
                            CorruptedLinesScore += settings.CorruptedScore;
                            return;
                        }
                        else
                        {
                            // The character matches, the most recently open chunk is closed properly.
                            openChunks.Pop();
                        }
                    }
                }
            }

            // We've reached the end of the line without encountering a corrupted character so it's incomplete.
            long total = 0;
            while (openChunks.Count > 0)
            {
                total = 5 * total + ChunkSettings[openChunks.Pop()].IncompleteScore;
            }
            IncompleteLineScores.Add(total);
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public long RunPart1()
        {
            return CorruptedLinesScore;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            IncompleteLineScores.Sort();

            return IncompleteLineScores[IncompleteLineScores.Count/2];
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
