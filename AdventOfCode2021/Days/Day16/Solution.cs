using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2021.BITS;

namespace AdventOfCode2021.Days.Day16
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 16;

        /// <summary>
        /// The packets.
        /// </summary>
        protected Packet Packet { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var parser = new PacketParser(Input);

            Packet = parser.ParseSingle();            
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public int RunPart1()
        {
            return Packet.SumVersions();
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            return Packet.GetValue();
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
