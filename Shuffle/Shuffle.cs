using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNumbersGame
    {
    class Shuffle
        {

        static void MatrixShuffle()
            {
            int counter = 0;
            do
                {
                Random random = new Random ();
                colStartIndex = random.Next (0, matrixSize - 1);
                rowStartIndex = random.Next (0, matrixSize - 1);
                PrintMatrixColor (matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, ConsoleColor.Blue);//cursor position control
                System.Threading.Thread.Sleep (600);
                PrintMatrixColor (matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, ConsoleColor.Red);
                ExchangeValuesInShuffling (matrix);
                System.Threading.Thread.Sleep (600);
                PrintMatrixColor (matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, ConsoleColor.Red);
                System.Threading.Thread.Sleep (1000);
                //PrintMatrixColor ( matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2 , ConsoleColor.Blue);//cursor position control
                counter++;
                } while ( counter < 10 );
            }

        private static void PrintMatrixColor(int matrixDimension, int[,] matrix, int cursorX, int cursorY, ConsoleColor color)
            {
            for ( int i = 0; i < matrixDimension; i++ )
                {
                Console.SetCursorPosition (cursorX, cursorY - 5 + i * 2);
                for ( int j = 0; j < matrixDimension; j++ )
                    {
                    if ( i >= rowStartIndex && i <= (rowStartIndex + 1) && j >= colStartIndex && j <= (colStartIndex + 1) )
                        {
                        Console.ForegroundColor = color;
                        }
                    else
                        {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    if ( matrix[i, j] == 0 )
                        {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write ("{0,4}", "\u2588\u2588");
                        Console.BackgroundColor = ConsoleColor.Black;
                        }
                    else
                        {
                        Console.Write ("{0,4}", matrix[i, j]);
                        }
                    }
                Console.WriteLine ();
                }
            Console.ForegroundColor = ConsoleColor.Gray;
            }

        private static void ExchangeValuesInShuffling(int[,] matrix)
            {
            int temp = matrix[rowStartIndex, colStartIndex];
            int temp2 = matrix[rowStartIndex + 1, colStartIndex];
            int temp3 = matrix[rowStartIndex + 1, colStartIndex + 1];
            int temp4 = matrix[rowStartIndex, colStartIndex + 1];

            if ( temp3 != 0 )
                {
                matrix[rowStartIndex, colStartIndex] = temp4;
                matrix[rowStartIndex + 1, colStartIndex] = temp;
                matrix[rowStartIndex + 1, colStartIndex + 1] = temp2;
                matrix[rowStartIndex, colStartIndex + 1] = temp3;
                }
            else
                {
                matrix[rowStartIndex, colStartIndex] = temp4;
                matrix[rowStartIndex + 1, colStartIndex] = temp;
                //matrix[rowStartIndex + 1, colStartIndex + 1] = temp2;
                matrix[rowStartIndex, colStartIndex + 1] = temp2;
                }
            }

        }
    }
