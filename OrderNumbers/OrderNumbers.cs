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
            //Random rand = new Random ();
            int[] numbers = new int[matrixSize * matrixSize];
            for (int k = 0; k < numbers.Length; k++)
            {
                numbers[k] = k + 1;
            }

            //numbers = numbers.OrderBy (x => rand.Next ()).ToArray ();    //shuffeling numbers
            numbers[numbers.Length - 1] = 0;
            int count = 0;

            int[,] matrix = new int[matrixSize, matrixSize];
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    matrix[i, j] = numbers[count];
                    count++;
                }
            }
            return matrix;
        }

        private static void ConsoleWindowSettings()
        {
            Console.Title = "..::: OrderNumbersGame :::..";
            Console.CursorVisible = false;
        }

        private static void StartExitChoiceMenu()
        {
             Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - "..::: SEQUENCE NUMBERS GAME :::..".Length) / 2,
                Console.WindowHeight / 2 - 5);
            Console.WriteLine("..::: ORDER NUMBERS GAME :::..");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 2);
            Console.WriteLine("Commands:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 16, Console.WindowHeight / 2);
            Console.WriteLine("to START new game - type \"start\"");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 16, Console.WindowHeight / 2 + 2);
            Console.WriteLine("for EXIT - type \"exit\"");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 + 4);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Enter command here:");

            Console.SetCursorPosition(Console.WindowWidth / 2 - 3, Console.WindowHeight / 2 + 5);
            string command = Console.ReadLine();

            try
            {
                if ((command != "start") && (command != "exit"))
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(Console.WindowWidth / 2 - 13, Console.WindowHeight / 2 + 7);
                Console.WriteLine("Invalid command! Input command again!");
                Thread.Sleep(800);
                Console.Clear();
                StartExitChoiceMenu();
            }

            if (command == "exit")
            {
                Environment.Exit(0);
            }

            Console.Clear();
        }

        private static void HelpTextShowHide()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
            Console.WriteLine("to SHOW Help menu press \"S\"");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 1);
            Console.WriteLine("to HIDE Help menu press \"H\"");
        }

        private static void PrintHelpMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 3);
            Console.WriteLine("Game OBJECTIVE: placing numbers");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 4);
            Console.WriteLine("from 1 to 15 in sequence,");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 5);
            Console.WriteLine("before the time is out!");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 6);
            Console.WriteLine("To MOVE numbers use ARROWS BUTTONS");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 7);
            Console.WriteLine("To move UPPER number press DOWN ARROW");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 8);
            Console.WriteLine("To move BOTTOM number press UP ARROW");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 9);
            Console.WriteLine("To move LEFT number press RIGHT ARROW");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2 + 10);
            Console.WriteLine("To move RIGHT number press LEFT ARROW");
        }

        private static int CheckOrderedElementsPercents(int[,] matrix)
        {
            int matrixDimention = matrix.GetLength(0);
            int currentOrderedElement = 0;
            int countOrderedElements = 0;
            for (int row = 0; row < matrixDimention - 1; row++)
            {
                for (int col = 0; col < matrixDimention; col++)
                {
                    currentOrderedElement++;
                    if (matrix[row, col] == currentOrderedElement)
                    {
                        countOrderedElements++;
                    }
                }
            }
            int orderedElementPercetage = (countOrderedElements * 100) / (int)Math.Pow(matrixDimention, 2);
            return orderedElementPercetage;
        }

        private static void PrintPercentageOfOrderedElements(int percentageOfOrderedElemnts, int coordinateX,
            int coordinateY)
        {
            Console.SetCursorPosition(coordinateX, coordinateY);
            Console.WriteLine("Ordered: {0}%", percentageOfOrderedElemnts);
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

