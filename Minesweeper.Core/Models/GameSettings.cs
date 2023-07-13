namespace Minesweeper.Core.Models
{
    public class GameSettings
    {
        public GameSettings(int height, int width, int mines)
        {
            Height = height;
            Width = width;
            Mines = mines;
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public int Mines { get; set; }
    }
}
