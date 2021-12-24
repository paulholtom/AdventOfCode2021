using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day24
{
    public class Solution : SolutionBase
    {
        /// <inheritdoc/>
        protected override int Day => 24;

        /// <summary>
        /// The MONAD alu.
        /// </summary>
        protected ALU MONAD { get; }

        /// <inheritdoc/>
        public Solution(string? input = null) : base(input)
        {
            var program = Input.SplitLines();

            MONAD = new ALU(program);
            /**
             * I solved this by reading the "input" code. I did create the ALU and use it to verify that my numbers were valid.
             * It uses 'w' as the input variable and 'z' as a sort of running total and x and y are used to manipulate data.
             * For each input it roughly does one of two things:
             * - multiply z by 26 and add the w + some constant which I'll call c to it 
             *          which is always less than 17 so the total is less than 26 OR
             * - divide z by 26 using integer division so the most recently added input becomes the remainder (which it puts in x)
             *      if that remainder + some addtional constant equals the current input nothing more is done, otherwise z is multiplied by 26 again and the whole thing will never reach zero
             * 
             * This results in pairs of digits that need to follow certain rules in order to be a valid model number.
             * I figured those out for my input by reading through the code, they are:
             * 
             * fifth + 7 = sixth
             * seventh - 5 = eigth
             * fourth - 8 = ninth
             * tenth - 4 = eleventh
             * third = twelfth
             * second + 5 = thirteenth
             * first - 1 = fourteenth
             * 
             * From here finding the largest and smallest number is just matter of setting one side of each of those equations to 9/1 such that the other side is between 1 and 9
             * 
             * Since it's unclear how other inputs vary it's unclear what the best general solution is but one that would probably always work in a resonable amount of time would be:
             * Zero out all but one digit then try to get a value of zero back by changing one other digit at a time. Do this until all of the pairs are discovered.
             */
        }

        /// <summary>
        /// Check if a number is a valid model number.
        /// </summary>
        /// <param name="num">The number to check.</param>
        /// <returns>True if it is, false if it isn't.</returns>
        public bool CheckValidModelNumber(long num)
        {
            foreach (var c in num.ToString())
            {
                MONAD.Inputs.Enqueue(c - '0');
            }
            MONAD.Run();
            return MONAD.Variables['z'] == 0;
        }

        /// <summary>
        /// Solve part 1.
        /// </summary>
        /// <returns>The solution for part 1</returns>
        public long RunPart1()
        {
            long value = 94992994195998;
            if (CheckValidModelNumber(value))
                return value;

            return 0;
        }

        /// <summary>
        /// Solve part 2.
        /// </summary>
        /// <returns>The solution for part 2.</returns>
        public long RunPart2()
        {
            long value = 21191861151161;
            if (CheckValidModelNumber(value))
                return value;

            return 0;
        }

        /// <inheritdoc/>
        public override string Run()
        {
            return GetOuputString(RunPart1().ToString(), RunPart2().ToString());
        }
    }
}
