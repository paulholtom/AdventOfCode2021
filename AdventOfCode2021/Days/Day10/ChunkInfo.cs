using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day10
{
    public class ChunkInfo
    {
        /// <summary>
        /// The opening character for the chunk.
        /// </summary>
        public char Open { get; }
        /// <summary>
        /// The closing character for the chunk.
        /// </summary>
        public char Close { get; }
        /// <summary>
        /// The amount to increase the score by if the closing character is the first corrupted character on a line.
        /// </summary>
        public int CorruptedScore { get; }
        /// <summary>
        /// The amount to add to the incomplete line score if this chunk is missing its closing character.
        /// </summary>
        public int IncompleteScore { get; }

        /// <summary>
        /// Create information about a type of chunk.
        /// </summary>
        /// <param name="open">The opening character.</param>
        /// <param name="close">The closing character.</param>
        /// <param name="corruptedScore">The score for a corrupted line discovered with this type of chunk.</param>
        /// <param name="incompleteScore">The score for an unclosed chunk of this type on an incomplete line.</param>
        public ChunkInfo(char open, char close, int corruptedScore, int incompleteScore)
        {
            Open = open;
            Close = close;
            CorruptedScore = corruptedScore;
            IncompleteScore = incompleteScore;
        }
    }
}
