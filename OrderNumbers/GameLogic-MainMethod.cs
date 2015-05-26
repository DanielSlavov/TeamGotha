using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderNumbersGame
    {
    class GameLogic_MainMethod
        {
        //static void Main()

        //    {
        //    while ( true )
        //        {
        //        ConsoleWindowSettings ();

        //        Console.BufferHeight = Console.WindowHeight = windowsHeight;
        //        Console.BufferWidth = Console.WindowWidth = windowsWidth;

        //        StartExitChoiceMenu ();
        //        HelpTextShowHide ();

        //        matrix = InicializeMatrix (matrixSize);
        //        maxTime = TimeSpan.FromMinutes (gameTimeLimitInMins);
        //        remainingTime = maxTime;
        //        MatrixShuffle ();

        //        startTime = DateTime.Now;


        //        /*
        //         * Start Game logic here
        //         */

        //        bool endOfGame = true;


        //        while ( (remainingTime.Minutes >= 0 && remainingTime.Seconds >= 0 && orderedElementsPercents < 100) )
        //            {
        //            orderedElementsPercents = CheckOrderedElementsPercents (matrix);
        //            PrintMatrix (matrixSize, matrix, Console.WindowWidth / 2 - 25, Console.WindowHeight / 2, posZeroX, posZeroY);
        //            PrintHelpNumbersArrows (posZeroX, posZeroY);
        //            remainingTime = RemainingTimeCalculate (maxTime);

        //            PrintTimer (remainingTime, Console.WindowWidth - 26, Console.WindowHeight - 30);
        //            PrintPercentageOfOrderedElements (orderedElementsPercents, Console.WindowWidth - 26, Console.WindowHeight - 32);

        //            try
        //                {
        //                ConsoleKeyInfo pressedKey = default ( ConsoleKeyInfo );//променлива в която присвояваме натиснатия клавиш
        //                while ( Console.KeyAvailable )
        //                    {
        //                    pressedKey = Console.ReadKey ();
        //                    switch ( pressedKey.Key )
        //                        {
        //                        case ConsoleKey.RightArrow:
        //                            if ( posZeroY > 0 )
        //                                {
        //                                matrix[posZeroX, posZeroY] = matrix[posZeroX, posZeroY - 1];
        //                                matrix[posZeroX, posZeroY - 1] = 0;
        //                                posZeroY--;
        //                                }
        //                            break;
        //                        case ConsoleKey.LeftArrow:
        //                            if ( posZeroY < 3 )
        //                                {
        //                                matrix[posZeroX, posZeroY] = matrix[posZeroX, posZeroY + 1];
        //                                matrix[posZeroX, posZeroY + 1] = 0;
        //                                posZeroY++;
        //                                }
        //                            break;
        //                        case ConsoleKey.DownArrow:
        //                            if ( posZeroX > 0 )
        //                                {
        //                                matrix[posZeroX, posZeroY] = matrix[posZeroX - 1, posZeroY];
        //                                matrix[posZeroX - 1, posZeroY] = 0;
        //                                posZeroX--;
        //                                }
        //                            break;
        //                        case ConsoleKey.UpArrow:
        //                            if ( posZeroX < 3 )
        //                                {
        //                                matrix[posZeroX, posZeroY] = matrix[posZeroX + 1, posZeroY];
        //                                matrix[posZeroX + 1, posZeroY] = 0;
        //                                posZeroX++;
        //                                }
        //                            break;
        //                        case ConsoleKey.S:
        //                            Console.Write ("\r");
        //                            Console.Write (' ');
        //                            HelpTextShowHide ();
        //                            PrintHelpMenu ();
        //                            break;
        //                        case ConsoleKey.H:
        //                            PrintHelpMenu ();
        //                            Console.Clear ();
        //                            HelpTextShowHide ();
        //                            break;
        //                        default:
        //                            throw new InvalidOperationException ();
        //                        }// end switch-case
        //                    } // end if selecting move
        //                }// end try start catch
        //            catch ( InvalidOperationException )
        //                {
        //                //Console.Clear ();
        //                Console.Write ("\r");
        //                Console.Write (' ');
        //                HelpTextShowHide ();
        //                continue;
        //                }//end try-catch

        //            Console.Write ("\r");
        //            Console.Write (' ');

        //            }// end while loop

        //        Console.Clear ();

        //        if ( orderedElementsPercents == 100 )
        //            {
        //            Console.ForegroundColor = ConsoleColor.DarkYellow;
        //            Console.SetCursorPosition (30, Console.WindowHeight / 2 - 6);
        //            Console.WriteLine ("YOU WIN!!!");
        //            Console.SetCursorPosition (13, Console.WindowHeight / 2 - 4);
        //            Console.WriteLine ("The puzel is {0}% ordered for {1} minutes!", orderedElementsPercents, TimeFromStartCalculation (maxTime, remainingTime));
        //            Console.SetCursorPosition (8, Console.WindowHeight / 2 + 10);
        //            break;
        //            }
        //        else if ( endOfGame == true )
        //            {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //            Console.SetCursorPosition (30, Console.WindowHeight / 2 - 6);
        //            Console.WriteLine ("GAME OVER!!!");
        //            Console.SetCursorPosition (13, Console.WindowHeight / 2 - 4);
        //            Console.WriteLine ("The puzel is {0}% ordered for {1} minutes!", orderedElementsPercents, TimeFromStartCalculation (maxTime, remainingTime));
        //            Console.SetCursorPosition (8, Console.WindowHeight / 2 + 10);
        //            }

        //        }
        //    }// end main
        }
    }
