
using System;
using System.Threading;
using System.Linq;
using System.Text;

namespace OrderNumbers
{
    internal class OrderNumbers
    {
        private const int windowsHeight = 40;
        private const int windowsWidth = 70;
        private static int rowStartIndex = 0;
        private static int colStartIndex = 0;
        private static DateTime startTime;
        private static int matrixSize = 4;
        private static int[,] matrix;
        private static int gameTimeLimitInMins = 10;
        private static TimeSpan maxTime;
        private static TimeSpan remainingTime;
        private static int orderedElementsPercents = 0;

        private static int posZeroX = 3;
        private static int posZeroY = 3;

        private static void Main()
        {
            while (true)
            {
                ConsoleWindowSettings();

                Console.BufferHeight = Console.WindowHeight = windowsHeight;
                Console.BufferWidth = Console.WindowWidth = windowsWidth;

                StartExitChoiceMenu();
                HelpTextShowHide();

                matrix = InicializeMatrix(matrixSize);
                maxTime = TimeSpan.FromMinutes(gameTimeLimitInMins);
                remainingTime = maxTime;
                MatrixShuffle();

                startTime = DateTime.Now;


                /*
                 * Start Game logic here
                 */

                bool endOfGame = true;


                while ((remainingTime.Minutes >= 0 && remainingTime.Seconds >= 0 && orderedElementsPercents < 100))
                {
                    orderedElementsPercents = CheckOrderedElementsPercents(matrix);
                    PrintMatrix(matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, posZeroX, posZeroY);
                    PrintHelpNumbersArrows(posZeroX, posZeroY);
                    remainingTime = RemainingTimeCalculate(maxTime);

                    PrintTimer(remainingTime, Console.WindowWidth - 26, Console.WindowHeight - 30);
                    PrintPercentageOfOrderedElements(orderedElementsPercents, Console.WindowWidth - 26, Console.WindowHeight - 32);

                    try
                    {
                        ConsoleKeyInfo pressedKey = default(ConsoleKeyInfo);//променлива в която присвояваме натиснатия клавиш
                        while (Console.KeyAvailable)
                        {
                            pressedKey = Console.ReadKey();
                            switch (pressedKey.Key)
                            {
                                case ConsoleKey.RightArrow:
                                    if (posZeroY > 0)
                                    {
                                        matrix[posZeroX, posZeroY] = matrix[posZeroX, posZeroY - 1];
                                        matrix[posZeroX, posZeroY - 1] = 0;
                                        posZeroY--;
                                    }
                                    break;
                                case ConsoleKey.LeftArrow:
                                    if (posZeroY < 3)
                                    {
                                        matrix[posZeroX, posZeroY] = matrix[posZeroX, posZeroY + 1];
                                        matrix[posZeroX, posZeroY + 1] = 0;
                                        posZeroY++;
                                    }
                                    break;
                                case ConsoleKey.DownArrow:
                                    if (posZeroX > 0)
                                    {
                                        matrix[posZeroX, posZeroY] = matrix[posZeroX - 1, posZeroY];
                                        matrix[posZeroX - 1, posZeroY] = 0;
                                        posZeroX--;
                                    }
                                    break;
                                case ConsoleKey.UpArrow:
                                    if (posZeroX < 3)
                                    {
                                        matrix[posZeroX, posZeroY] = matrix[posZeroX + 1, posZeroY];
                                        matrix[posZeroX + 1, posZeroY] = 0;
                                        posZeroX++;
                                    }
                                    break;
                                case ConsoleKey.S:
                                    Console.Write("\r");
                                    Console.Write(' ');
                                    HelpTextShowHide();
                                    PrintHelpMenu();
                                    break;
                                case ConsoleKey.H:
                                    PrintHelpMenu();
                                    Console.Clear();
                                    HelpTextShowHide();
                                    break;
                                default:
                                    throw new InvalidOperationException();
                            }// end switch-case
                        } // end if selecting move
                    }// end try start catch
                    catch (InvalidOperationException)
                    {
                        //Console.Clear ();
                        Console.Write("\r");
                        Console.Write(' ');
                        HelpTextShowHide();
                        continue;
                    }//end try-catch

                    Console.Write("\r");
                    Console.Write(' ');

                }// end while loop

                Console.Clear();

                if (orderedElementsPercents == 100)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.SetCursorPosition(30, Console.WindowHeight / 2 - 6);
                    Console.WriteLine("YOU WIN!!!");
                    Console.SetCursorPosition(13, Console.WindowHeight / 2 - 4);
                    Console.WriteLine("The puzel is {0}% ordered for {1} minutes!", orderedElementsPercents, TimeFromStartCalculation(maxTime, remainingTime));
                    Console.SetCursorPosition(8, Console.WindowHeight / 2 + 10);
                    break;
                }
                else if (endOfGame == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(30, Console.WindowHeight / 2 - 6);
                    Console.WriteLine("GAME OVER!!!");
                    Console.SetCursorPosition(13, Console.WindowHeight / 2 - 4);
                    Console.WriteLine("The puzel is {0}% ordered for {1} minutes!", orderedElementsPercents, TimeFromStartCalculation(maxTime, remainingTime));
                    Console.SetCursorPosition(8, Console.WindowHeight / 2 + 10);
                }

            }
        } // end main


        /*
         * Method Declarations to follow
         */

        private static void MatrixShuffle()
        {
            int counter = 0;
            do
            {
                Random random = new Random();
                colStartIndex = random.Next(0, matrixSize - 1);
                rowStartIndex = random.Next(0, matrixSize - 1);
                PrintMatrixColor(matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, ConsoleColor.Blue);//cursor position control
                System.Threading.Thread.Sleep(600);
                PrintMatrixColor(matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, ConsoleColor.Red);
                ExchangeValuesInShuffling(matrix);
                System.Threading.Thread.Sleep(600);
                PrintMatrixColor(matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, ConsoleColor.Red);
                System.Threading.Thread.Sleep(1000);
                //PrintMatrixColor ( matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2 , ConsoleColor.Blue);//cursor position control
                counter++;
            } while (counter < 10);
        }

        private static void PrintMatrixColor(int matrixDimension, int[,] matrix, int cursorX, int cursorY, ConsoleColor color)
        {
            for (int i = 0; i < matrixDimension; i++)
            {
                Console.SetCursorPosition(cursorX, cursorY - 5 + i * 2);
                for (int j = 0; j < matrixDimension; j++)
                {
                    if (i >= rowStartIndex && i <= (rowStartIndex + 1) && j >= colStartIndex && j <= (colStartIndex + 1))
                    {
                        Console.ForegroundColor = color;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    if (matrix[i, j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("{0,4}", "\u2588\u2588");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write("{0,4}", matrix[i, j]);
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ExchangeValuesInShuffling(int[,] matrix)
        {
            int temp = matrix[rowStartIndex, colStartIndex];
            int temp2 = matrix[rowStartIndex + 1, colStartIndex];
            int temp3 = matrix[rowStartIndex + 1, colStartIndex + 1];
            int temp4 = matrix[rowStartIndex, colStartIndex + 1];

            if (temp3 != 0)
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
        private static void PrintMatrix(int matrixDimension, int[,] matrix, int cursorX, int cursorY, int row, int col)//
        {
            for (int i = 0; i < matrixDimension; i++)
            {
                Console.SetCursorPosition(cursorX, cursorY - 5 + i * 2);
                for (int j = 0; j < matrixDimension; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (matrix[i, j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("{0,4}", "\u2588\u2588");
                    }
                    else if ((i == row - 1) && (j == col) || (i == row) && (j == col - 1) || (i == row) && (j == col + 1) ||
                             (i == row + 1) && (j == col))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("{0,4}", matrix[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,4}", matrix[i, j]);
                    }
                } // end inner for loop
                Console.WriteLine();
            } // end for loop
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void PrintHelpNumbersArrows(int indZeroX, int indZeroY)//
        {
            if (indZeroY != 0)
            {
                int temp = matrix[indZeroX, indZeroY - 1];
                PrintActiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 5, temp, "RIGHT");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                PrintInactiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 5, "RIGHT");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (indZeroY != 3)
            {
                int temp = matrix[indZeroX, indZeroY + 1];
                PrintActiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 4, temp, "LEFT");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                PrintInactiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 4, "LEFT");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (posZeroX != 3)
            {
                int temp = matrix[indZeroX + 1, indZeroY];
                PrintActiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 3, temp, "UP");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                PrintInactiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 3, "UP");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if (posZeroX != 0)
            {
                int temp = matrix[indZeroX - 1, indZeroY];
                PrintActiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 2, temp, "DOWN");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                PrintInactiveArrows(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 - 2, "DOWN");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        private static void PrintActiveArrows(int x, int y, int matrixElement, string arrow)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("{0,-2}", matrixElement);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" : press ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(arrow);
        }

        private static void PrintInactiveArrows(int x, int y, string arrow)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("{0,-2}", "--");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" : press ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(arrow);
        }


        private static int[,] InicializeMatrix(int matrixSize)
        {

        }

        private static void ConsoleWindowSettings()
        {

        }

        private static void StartExitChoiceMenu()
        {

        }

        private static void HelpTextShowHide()
        {

        }

        private static void PrintHelpMenu()
        {

        }

        private static int CheckOrderedElementsPercents(int[,] matrix)
        {

        }

        private static void PrintPercentageOfOrderedElements(int percentageOfOrderedElemnts, int coordinateX,
            int coordinateY)
        {

        }

        private static void PrintTimer(TimeSpan timeLeft, int coordinateX, int coordinateY)
        {
            Console.SetCursorPosition(coordinateX, coordinateY);
            Console.WriteLine("Time left:");
            Console.SetCursorPosition(coordinateX + 3, coordinateY + 2);
            if (timeLeft.Minutes == 0 && timeLeft.Seconds == -1)
            {
                Console.WriteLine("00:00");
            }
            else
            {
                Console.WriteLine("{0,2:00}:{1,2:00}", timeLeft.Minutes, timeLeft.Seconds);
            }
        }

        private static TimeSpan RemainingTimeCalculate(TimeSpan maxTime)
        {
            TimeSpan remainingTimeTS = maxTime - (DateTime.Now - startTime);
            return remainingTimeTS;
        }

        private static string TimeFromStartCalculation(TimeSpan maxTime, TimeSpan remainingTime)
        {
            TimeSpan spentTime = maxTime - remainingTime - TimeSpan.FromSeconds(1);

            string elapsedTime = string.Format("{0,2:00}:{1,2:00}", spentTime.Minutes, spentTime.Seconds);
            return elapsedTime;
        }

    }
}

