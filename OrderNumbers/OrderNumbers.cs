
using System;
using System.Threading;
using System.Linq;
using System.Text;

namespace OrderNumberGame
{
    class Puzzel
    {
        const int windowsHeight = 40;
        const int windowsWidth = 70;
        static int rowStartIndex = 0;
        static int colStartIndex = 0;
        static DateTime startTime;
        static int matrixSize = 4;
        static int[,] matrix;
        static int gameTimeLimitInMins = 10;
        static TimeSpan maxTime;
        static TimeSpan remainingTime;
        static int orderedElementsPercents = 0;

        static int posZeroX = 3;
        static int posZeroY = 3;

        static void Main()
        {

        }// end main


        /*
         * Method Declarations to follow
         */

        static void MatrixShuffle()
        {

        }

        private static void PrintMatrixColor(int matrixDimension, int[,] matrix, int cursorX, int cursorY, ConsoleColor color)
        {

        }

        private static void PrintMatrix(int matrixDimension, int[,] matrix, int cursorX, int cursorY, int row, int col)
        {

        }

        private static void PrintHelpNumbersArrows(int indZeroX, int indZeroY)
        {

        }

        private static void PrintActiveArrows(int x, int y, int matrixElement, string arrow)
        {

        }

        private static void PrintInactiveArrows(int x, int y, string arrow)
        {

        }

        private static void ExchangeValuesInShuffling(int[,] matrix)
        {

        }

        private static int[,] InicializeMatrix(int matrixSize)
        {

        }

        static void ConsoleWindowSettings()
        {

        }

        static void StartExitChoiceMenu()
        {

        }

        static void HelpTextShowHide()
        {

        }

        static void PrintHelpMenu()
        {

        }

        private static int CheckOrderedElementsPercents(int[,] matrix)
        {

        }

        private static void PrintPercentageOfOrderedElements(int percentageOfOrderedElemnts, int coordinateX, int coordinateY)
        {

        }

        private static void PrintTimer(TimeSpan timeLeft, int coordinateX, int coordinateY)
        {

        }

        private static TimeSpan RemainingTimeCalculate(TimeSpan maxTime)
        {

        }

        private static string TimeFromStartCalculation(TimeSpan maxTime, TimeSpan remainingTime)
        {

        }

    }
}

