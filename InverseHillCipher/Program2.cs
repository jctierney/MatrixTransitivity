/******************************************************************************
 * File: Program2.cs
 * Description: The same as Program.cs, but formatted using Java style to
 *              slim the line count down.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverseHillCipher {
    public class Program2 {
        /// <summary>
        /// Main entry point to the program.
        /// </summary>
        static void Main(string[] args){
            int num;
            Console.WriteLine("Welcome!");
            Console.Write("Please enter the number: ");
            string numStr = Console.ReadLine();
            if (!Int32.TryParse(numStr, out num))
                Console.WriteLine("Invalid number...");
            for (int j = 0; j < num; j++) {
                for (int k = 0; k < 100; k++) {
                    double temp = 0;
                    if (j > 0) {
                        temp = ((double)num * (double)k + (double)1) / (double)j;
                        if (IsInteger(temp) && temp < num)
                            Console.WriteLine("{0}: {1}", j, temp);
                    }
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Determines if the given number is an integer.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static bool IsInteger(double num) {
            if (num == (int)num)
                return true;
            return false;
        }
    }
}