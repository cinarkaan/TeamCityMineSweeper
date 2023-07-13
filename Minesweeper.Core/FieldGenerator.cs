namespace Minesweeper.Core
{
    public static class FieldGenerator
    {
        public static bool[,] GetRandomField(int sizeX, int sizeY, int mineCount)
        {
            var list = Enumerable.Range(0, sizeX * sizeY);

            var randomized = list.OrderBy(x => Guid.NewGuid()).ToArray();

            var mineElements = randomized.Take(mineCount);

            var result = new bool[sizeY, sizeX];

            foreach (var element in mineElements)
            {
                int row = element / sizeX;
                int col = element % sizeX;

                result[row, col] = true;
            }


            return result;
        }
    }
}
