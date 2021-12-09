using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class Submarine
    {
        /// <summary>
        /// The forward instruction.
        /// </summary>
        public const string FORWARD = "forward";
        /// <summary>
        /// The up instruction.
        /// </summary>
        public const string UP = "up";
        /// <summary>
        /// The down instruction
        /// </summary>
        public const string DOWN = "down";

        /// <summary>
        /// The current depth;
        /// </summary>
        public int Depth { get; set; } = 0;

        /// <summary>
        /// The current horizontal positoin.
        /// </summary>
        public int HorizontalPosition { get; set; } = 0;

        /// <summary>
        /// The current aim.
        /// </summary>
        public int Aim { get; set; } = 0;

        /// <summary>
        /// Run a set of instructions.
        /// </summary>
        /// <param name="instructions">The instructions to run.</param>
        public void RunInstructions(params string[] instructions)
        {
            foreach (var instruction in instructions)
            {
                var instructionSplit = instruction.Split(new char[] { ' ' }, 2);
                var parameters = instructionSplit[1];
                switch (instructionSplit[0])
                {
                    case FORWARD:
                        SingleIntParam(parameters, Forward);
                        break;
                    case UP:
                        SingleIntParam(parameters, Up);
                        break;
                    case DOWN:
                        SingleIntParam(parameters, Down);
                        break;
                }
            }
        }

        /// <summary>
        /// Handle an instruction expecting a single integer parameter.
        /// </summary>
        /// <param name="param">The string for the parameters.</param>
        /// <param name="action">The action expecting the parameter.</param>
        protected void SingleIntParam(string param, Action<int> action)
        {
            action(int.Parse(param));
        }

        /// <summary>
        /// Execute the forward instruction.
        /// </summary>
        /// <param name="magnitude">The magnitude of the change</param>
        protected virtual void Forward(int magnitude)
        {
            HorizontalPosition += magnitude;
            Depth += magnitude * Aim;
        }

        /// <summary>
        /// Execute the up instruction.
        /// </summary>
        /// <param name="magnitude">The magnitude of the change</param>
        protected virtual void Up(int magnitude)
        {
            Aim -= magnitude;
        }

        /// <summary>
        /// Execute the down instruction.
        /// </summary>
        /// <param name="magnitude">The magnitude of the change</param>
        protected virtual void Down(int magnitude)
        {
            Aim += magnitude;
        }
    }
}
