namespace Minesweeper.Core.Models
{
    public class Point
    {
        public bool IsMine { get; set; }

        public bool IsOpen { get; set; }

        public int MineNeighborsCount { get; set; }

        public bool IsFlag { get; set; }
    }
}
