namespace Minesweeper.Core.Enums
{
    public enum PointState
    {
        // opened cell without mine neighbors
        Neighbors0,

        // opened cell with 1 mine neighbor
        Neighbors1,

        // opened cell with 2 mine neighbor
        Neighbors2,

        // opened cell with 3 mine neighbor
        Neighbors3,

        // opened cell with 4 mine neighbor
        Neighbors4,

        // opened cell with 5 mine neighbor
        Neighbors5,

        // opened cell with 5 mine neighbor
        Neighbors6,

        // opened cell with 6 mine neighbor
        Neighbors7,

        // opened cell with 7 mine neighbor
        Neighbors8,

        // closed cell
        Close,

        // opened cell with mine
        Mine
    }
}
