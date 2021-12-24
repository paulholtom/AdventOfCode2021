using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day24
{
    /// <summary>
    /// An arithmetic logic unit.
    /// </summary>
    public class ALU
    {
        /// <summary>
        /// The program to be run.
        /// </summary>
        protected string[] Program { get; }

        /// <summary>
        /// The varaible values.
        /// </summary>
        public Dictionary<char, long> Variables { get; }

        /// <summary>
        /// The inputs.
        /// </summary>
        public Queue<long> Inputs { get; }

        /// <summary>
        /// Constructor, takes a program.
        /// </summary>
        /// <param name="program">The program this will run.</param>
        public ALU(string[] program)
        {
            Program = program;

            Variables = new Dictionary<char, long>()
            {
                {'x', 0 },
                {'y', 0 },
                {'z', 0 },
                {'w', 0 },
            };

            Inputs = new();

            Initialize();
        }

        /// <summary>
        /// Initialize before a run.
        /// </summary>
        protected void Initialize()
        {
            foreach(var key in Variables.Keys)
            {
                Variables[key] = 0;
            }
        }

        /// <summary>
        /// Perform a binary operation.
        /// </summary>
        /// <param name="firstParam">The first parameter for the operation. This must always be one of the variables.</param>
        /// <param name="secondParam">The second parameter for the operation. This can either be a variable or a value.</param>
        /// <param name="op">The operation. The return value will be assigned to the variable for the first parameter.</param>
        protected void RunBinaryOp(string firstParam, string secondParam, Func<long, long, long> op)
        {
            var mainVar = firstParam[0];

            long secondVal;
            if (Variables.ContainsKey(secondParam[0]))
                secondVal = Variables[secondParam[0]];
            else
                secondVal = long.Parse(secondParam);

            Variables[mainVar] = op(Variables[mainVar], secondVal);
        }

        /// <summary>
        /// Run a single instruction.
        /// </summary>
        /// <param name="instruction">The instruction to run.</param>
        protected void RunInstruction(string instruction)
        {
            var instructionSplit = instruction.Split(' ');

            switch (instructionSplit[0])
            {
                case "inp":
                    Variables[instructionSplit[1][0]] = Inputs.Dequeue();
                    break;
                case "add":
                    RunBinaryOp(instructionSplit[1], instructionSplit[2], (a, b) => a + b);
                    break;
                case "mul":
                    RunBinaryOp(instructionSplit[1], instructionSplit[2], (a, b) => a * b);
                    break;
                case "div":
                    // C#'s integer division already gets a result that rounds towards zero.
                    RunBinaryOp(instructionSplit[1], instructionSplit[2], (a, b) => a / b);
                    break;
                case "mod":
                    RunBinaryOp(instructionSplit[1], instructionSplit[2], (a, b) => a % b);
                    break;
                case "eql":
                    RunBinaryOp(instructionSplit[1], instructionSplit[2], (a, b) => a == b ? 1 : 0);
                    break;
            }
        }

        /// <summary>
        /// Run the program.
        /// </summary>
        public void Run()
        {
            Initialize();
            foreach(var instruction in Program)
            {
                RunInstruction(instruction);
            }
        }
    }
}
