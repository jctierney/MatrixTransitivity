/******************************************************************************
 * Copyright (c) 2013 Jason Tierney
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MatrixTransitive
{
    public class MatrixTransitivity
    {
        /// <summary>
        /// Main entry point for the program.
        /// </summary>
        private static void Main(string[] args)
        {
            string fileName;
            string tryagain = "y";
            Console.WriteLine("Welcome!");
            
            while (tryagain.ToUpper().Equals("Y"))
            {
                Console.WriteLine("Please enter a file name: ");
                fileName = Console.ReadLine();
                ParseFile(fileName);
                Console.WriteLine("Would you like another file (Y/N)?");
                tryagain = Console.ReadLine();
            }
        }

        /// <summary>
        /// Parses a file and creates matrices from it.
        /// </summary>
        /// <param name="path"></param>
        private static void ParseFile(string path)
        {           
            string[] lines = File.ReadAllLines(path);
            int[,] matrix = new int[100, 100];
            int size = 0;
            int place = 0;
            
            for (int i = 0; i < lines.Length; i++)
            {
                string[] places = lines[i].Split(',');               
                if (lines[i].Equals("E"))
                {
                    PrintMatrix(matrix, size);                   
                    matrix = new int[100, 100];
                    place = -1;
                    size = -1;
                }
                
                for (int j = 0; j < places.Length; j++)
                {
                    int index = 0;                   
                    if (Int32.TryParse(places[j], out index))
                    {
                        matrix[place, index] = 1;
                    }                
                }

                place++;
                size++;
            }
        }

        /// <summary>
        /// Prints the matrix.
        /// </summary>
        /// <param name="matrix">The matrix that we want to print.</param>
        private static void PrintMatrix(int[,] matrix, int size)
        {
            Console.Write("[");
            
            for (int i = 0; i < size; i++)
            {
                Console.Write("[");
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j].ToString() + " ");
                }

                if (i == (size - 1))
                {
                    Console.Write("]");
                }
                else
                {
                    Console.WriteLine("]");
                }
            }

            Console.WriteLine("]");
            string error = string.Empty;

            if (IsTransitive(matrix, size, out error))
            {
                Console.WriteLine("Matrix is transitive...");
            }
            else
            {
                Console.WriteLine("Matrix is not transitive...");
                Console.WriteLine(error);
            }
        }

        /// <summary>
        /// Determines if the specified matrix is transitive in nature.
        /// </summary>
        /// <param name="matrix">Matrix we are checking</param>
        /// <param name="size">Size of the matrix</param>
        /// <returns>True if the matrix is transitive. False if it isn't.</returns>
        private static bool IsTransitive(int[,] matrix, int size, out string error)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] == 1)
                    {          
                        for (int k = 0; k < size; k++)
                        {
                            if (matrix[j, k] == 1 && matrix[i, k] == 0)
                            {
                                error = "Matrix fails because " + i + ", " + 
                                    j + " is related to " + j + ", " + k + 
                                    " but not to " + i + ", " + k + ".";
                                return false;
                            }
                        }
                    }
                }
            }

            error = string.Empty;
            return true;
        }
    }
}        