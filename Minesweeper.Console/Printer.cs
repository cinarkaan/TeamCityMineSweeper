using Minesweeper.Core.Enums;
using System.Drawing;

namespace Minesweeper.Console
{
    internal static class Printer
    {
        private static Dictionary<PointState, string> _consoleSymbolByPointState = new Dictionary<PointState, string>()
        {
            [PointState.Close] = "D  ",
            [PointState.Mine] = "*  ",
            [PointState.Neighbors0] = "-  ",
            [PointState.Neighbors1] = "1  ",
            [PointState.Neighbors2] = "2  ",
            [PointState.Neighbors3] = "3  ",
            [PointState.Neighbors4] = "4  ",
            [PointState.Neighbors5] = "5  ",
            [PointState.Neighbors6] = "6  ",
            [PointState.Neighbors7] = "7  ",
            [PointState.Neighbors8] = "8  ",
        };

        internal static DifficultyLevel ChooseDifficultyLevel()
        {
            System.Console.WriteLine("Choose difficulty level (Begginer|Intermediate|Expert):");

            DifficultyLevel difficultyLevel = Enum.Parse<DifficultyLevel>(System.Console.ReadLine());

            return difficultyLevel;
        }

        internal static Point GetCoordinates()
        {
            System.Console.WriteLine("Enter coordinate X");
            var x = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Enter coordinate Y");
            var y = int.Parse(System.Console.ReadLine());

            return new Point(x, y);
        }

        internal  static void PrintField(PointState[,] field)
        {
            System.Console.Clear();

            for (var row = field.GetLength(0) - 1; row >= 0; row--)
            {
                System.Console.Write($"{row.ToString("D2")} ");
                for (var column = 0; column < field.GetLength(1); column++)
                {
                    System.Console.Write(_consoleSymbolByPointState[field[row, column]]);
                }

                System.Console.WriteLine();
            }

            System.Console.Write("   ");

            for (var column = 0; column < field.GetLength(1); column++)
            {
                System.Console.Write($"{column.ToString("D2")} ");
            }

            System.Console.WriteLine();
        }

        internal static void PrintGameResult(GameState gameState)
        {
            System.Console.WriteLine(gameState == GameState.Lose ? "GAME IS OVER" : "YOU WIN!");

            System.Console.ReadKey();
        }
    }
}
