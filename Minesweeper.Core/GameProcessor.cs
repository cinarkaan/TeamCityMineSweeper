using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using System.Data.Common;

namespace Minesweeper.Core
{
    public class GameProcessor
    {
        private readonly Point[,] _field;
        public GameState GameState { get; private set; } = GameState.Active;
        private readonly int mineCount;
        private readonly int totalCount;
        private int openCount;

        public GameProcessor(bool[,] boolField)
        {
            _field = new Point[boolField.GetLength(0), boolField.GetLength(1)];

            for (var row = 0; row < boolField.GetLength(0); row++)
            {
                for (var column = 0; column < boolField.GetLength(1); column++)
                {
                    bool isMine = boolField[row, column];

                    _field[row, column] = new Point {IsMine = isMine };
                    mineCount = mineCount + (isMine ? 1: 0);
                }
            }

            totalCount = boolField.GetLength(0) * boolField.GetLength(1);
        }

        public GameState Open(int x, int y)
        {
            if (GameState != GameState.Active)
                throw new InvalidOperationException($"Game status is {GameState}");

            var targetCell = _field[y, x];

            if (targetCell.IsOpen)
                return GameState;

            targetCell.IsOpen = true;

            if (targetCell.IsMine)
            {
                GameState = GameState.Lose;
            }
            else
            {
                for (var row = Math.Max(0, y - 1); row <= Math.Min(_field.GetLength(0) - 1, y + 1); row++)
                {
                    for (var column = Math.Max(0, x - 1); column <= Math.Min(_field.GetLength(1) - 1, x + 1); column++)
                    {
                        Point neighbor = _field[row, column];

                        if (neighbor.IsMine)
                        {
                            targetCell.MineNeighborsCount++;
                        }
                    }
                }

                if (targetCell.MineNeighborsCount == 0)
                {
                    for (var row = Math.Max(0, y - 1); row <= Math.Min(_field.GetLength(0) - 1, y + 1); row++)
                    {
                        for (var column = Math.Max(0, x - 1); column <= Math.Min(_field.GetLength(1) - 1, x + 1); column++)
                        {
                            Open(column, row);
                        }
                    }
                }

                openCount++;

                if (openCount + mineCount == totalCount)
                {
                    GameState = GameState.Win;
                }
            }

            return GameState;
        }

        public PointState[,] GetCurrentField()
        {
            var publicFieldInfo = new PointState[_field.GetLength(0), _field.GetLength(1)];
            
            for (var row = 0; row < _field.GetLength(0); row++)
            {
                for (var column = 0; column < _field.GetLength(1); column++)
                {
                    var targetCell = _field[row, column];

                    if (!targetCell.IsOpen && GameState == GameState.Active)
                        publicFieldInfo[row, column] = PointState.Close;
                    else if (targetCell.IsMine)
                        publicFieldInfo[row, column] = PointState.Mine;
                    else
                        publicFieldInfo[row, column] = (PointState)targetCell.MineNeighborsCount;
                }
            }
            return publicFieldInfo;
        }
    }
}
