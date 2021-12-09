using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day2
{
    /// <summary>
    /// The incorrect submarine with no aim.
    /// </summary>
    public class SubmarineNoAim: Submarine
    {
        /// <inheritdoc/>
        protected override void Forward(int magnitude)
        {
            HorizontalPosition += magnitude;
        }

        /// <inheritdoc/>
        protected override void Up(int magnitude)
        {
            Depth -= magnitude;
        }

        /// <inheritdoc/>
        protected override void Down(int magnitude)
        {
            Depth += magnitude;
        }
    }
}
