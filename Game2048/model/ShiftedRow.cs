namespace Game2048.model
{
    /// <summary>
    /// class that represent one value of the lookup table
    /// instance of this class holds information about a single row in the board
    /// and the result of shift this row to the left
    /// </summary>
    class ShiftedRow
    {
        private ushort _shifted; // the row after shift left
        private int _emptyCells; // number of empty cells in the original row
        private int _mergeablesCells; // number of mergeable cells in the original row
        private uint _score; // the score of the row
        private bool _isWin; // if the row is win

        /// <summary>
        /// constructor that creates new instance of this class
        /// and set its fields to the parameters that received
        /// </summary>
        /// <param name="emptyCells">number of empty cells in the original row</param>
        /// <param name="shifted">the row after shift left</param>
        /// <param name="merges">number of mergeable cells in the original row</param>
        /// <param name="score">the score of the row</param>
        /// <param name="isWin">if the row is win</param>
        public ShiftedRow(int emptyCells, ushort shifted, int merges, uint score, bool isWin)
        {
            _emptyCells = emptyCells;
            _shifted = shifted;
            _mergeablesCells = merges;
            _score = score;
            _isWin = isWin;
        }

        /// <summary>
        /// property that returns instance to the row after shift left 
        /// to get or set its value
        /// </summary>
        public ushort Shifted
        {
            get { return _shifted; }
            set { _shifted = value; }
        }

        /// <summary>
        /// property that returns instance to the number of empty cells 
        /// to get or set its value
        /// </summary>
        public int EmptyCells
        {
            get { return _emptyCells; }
            set { _emptyCells = value; }
        }

        /// <summary>
        /// property that returns instance to the number of mergeable cells 
        /// to get or set its value
        /// </summary>
        public int MergeableCells
        {
            get { return _mergeablesCells; }
            set { _mergeablesCells = value; }
        }

        /// <summary>
        /// property that returns instance to the score of the row 
        /// to get or set its value
        /// </summary>
        public uint Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// property that returns instance to if the board is win 
        /// to get or set its value
        /// </summary>
        public bool IsWin
        {
            get { return _isWin; }
            set { _isWin = value; }
        }
    }
}
