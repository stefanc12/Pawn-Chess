namespace ChessLogic
{
    public class Direction
    {
        public readonly static Direction Nord = new Direction(-1, 0);
        public readonly static Direction Sud = new Direction(1, 0);
        public readonly static Direction Est = new Direction(0, 1);
        public readonly static Direction Vest = new Direction(0, -1);
        public readonly static Direction NordEst = Nord + Est;
        public readonly static Direction NordVest = Nord + Vest;
        public readonly static Direction SudEst = Sud + Est;
        public readonly static Direction SudVest = Sud + Vest;

        public int RowDelta {  get; }
        public int ColumnDelta { get; }

        public Direction(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }

        public static Direction operator +(Direction dir1, Direction dir2) 
        {
            return new Direction(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);
        }

        public static Direction operator *(int scalar, Direction dir)
        {
            return new Direction (scalar * dir.RowDelta, scalar * dir.ColumnDelta);
        }
    }
}
