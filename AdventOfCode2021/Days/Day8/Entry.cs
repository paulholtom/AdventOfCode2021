using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days.Day8
{
    public class Entry
    {
        /// <summary>
        /// The input "digits".
        /// 
        /// This should contain 10 unique entries, representing each digit from 0 to 9.
        /// </summary>
        public string[] Input { get; }
        /// <summary>
        /// The output "digits".
        /// </summary>
        public string[] Output { get; }

        /// <summary>
        /// Constructor for a single entry.
        /// </summary>
        /// <param name="input"></param>
        public Entry(string input)
        {
            var inOut = input.Split('|');

            Input = inOut[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Output = inOut[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Find the number of characters that match between two strings.
        /// </summary>
        /// <param name="strA">The first string.</param>
        /// <param name="strB">The second string.</param>
        /// <returns>The number of characters that appear in both strings.</returns>
        public int NumMatching(string strA, string strB)
        {
            int count = 0;
            foreach(char c in strA)
            {
                if (strB.Contains(c))
                    ++count;
            }
            return count;
        }

        /// <summary>
        /// Determine the number for the output.
        /// </summary>
        /// <returns>The output value.</returns>
        public int ParseOutput()
        {
            Dictionary<int, List<string>> byLength = new();

            foreach (string str in Input)
            {
                if (!byLength.ContainsKey(str.Length))
                    byLength.Add(str.Length, new List<string>());

                byLength[str.Length].Add(str);
            }

            string val1 = byLength[2][0];
            string val4 = byLength[4][0];

            int outputNum = 0;
            for (int i = 0; i < Output.Length; ++i)
            {
                var str = Output[i];
                int num = 0;
                switch (str.Length)
                {
                    case 2:
                        num = 1;
                        break;
                    case 3:
                        num = 7;
                        break;
                    case 4:
                        num = 4;
                        break;
                    case 7:
                        num = 8;
                        break;
                    case 5:
                        // 3 is the only one of length 5 that contains all (2) of the characters for 1
                        if (NumMatching(val1, str) == 2)
                            num = 3;
                        // 5 is the only one with 5 characters that contains 3 of the 4 character for 4
                        else if (NumMatching(val4, str) == 3)
                            num = 5;
                        // Any other one with 5 characters must be 2.
                        else
                            num = 2;
                        break;
                    case 6:
                        // 6 is the only one with 6 characters that doesn't contain both of the characters in 1.
                        if (NumMatching(val1, str) != 2)
                            num = 6;
                        // Of the remaining options 0 is the only one that doesn't contain all of the characters in 4.
                        else if (NumMatching(val4, str) != 4)
                            num = 0;
                        // 9 is all that's left.
                        else
                            num = 9;
                        break;
                }
                outputNum += (int)Math.Pow(10, Output.Length - i - 1) * num;
            }

            return outputNum;
        }
    }
}
