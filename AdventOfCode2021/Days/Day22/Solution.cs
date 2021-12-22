using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day22
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 22;

        /// <summary>
        /// The list of instructions.
        /// </summary>
        public List<Cuboid> Instructions { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            Instructions = new List<Cuboid>();
            foreach (var line in Input.SplitLines())
            {
                Instructions.Add(new Cuboid(line));
            }
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public long RunPart1()
        {
            ReactorState reactor = new ReactorState();
            foreach (var instruction in Instructions)
            {
                // Don't run any instructions outside of the initialization area.
                if (instruction.MinX > 50 || instruction.MaxX < -50 || instruction.MinY > 50 || instruction.MaxY < -50 || instruction.MinZ > 50 || instruction.MaxZ < -50)
                    continue;
                reactor.ApplyCuboid(instruction);
            }
            return reactor.GetOnCount();
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            ReactorState reactor = new ReactorState();
            foreach (var instruction in Instructions)
            {
                reactor.ApplyCuboid(instruction);
            }
            return reactor.GetOnCount();
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
