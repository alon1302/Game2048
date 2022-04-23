using System;
using System.Collections.Generic;

namespace Game2048.model
{
    class BitBoard : ICloneable
    {
        public const int ROW_SIZE = 4; // the size of a row in the board
        private const int BITS_OF_CELL = 4; // number of bits that each cell take
        private static Random random = new Random(); // randomizer

        private ulong _bitBoard; // number that represent the board

        private uint _score; // the current score of the board
        private int _emptyCells; // number of empty cells in the board
        private bool _isWon; // winning board or no

        private LookupTable lookupTable = LookupTable.Instance; // pointer to the global lookup table

        /// <summary>
        /// constructor that creates empty board
        /// </summary>
        public BitBoard()
        {
            this._bitBoard = 0;
            this._score = 0;
            this._isWon = false;
            this._emptyCells = ROW_SIZE * ROW_SIZE;
        }

        /// <summary>
        /// indexer that gives easy access to a wanted cell in the board to get or set its value
        /// </summary>
        /// <param name="row">row of the wanted cell</param>
        /// <param name="col">colomn of the wanted cell</param>
        /// <returns>the value of the wanted cell</returns>
        public int this[int row, int col]
        {
            get
            {
                int position = row * ROW_SIZE + col; // get the current cell position on the board
                ulong wantedCellMask = 0xfUL << BITS_OF_CELL * position; // create mask of 1 in the position of the wanted cell on the board
                ulong onlyWantedCell = _bitBoard & wantedCellMask; // turn off all the bits except the bits of the wanted cell 
                ulong wantedShifted = onlyWantedCell >> BITS_OF_CELL * position; // shift the wanted cell to the right of the number
                return (int)wantedShifted; // return the wanted cell in int format
            }
            set
            {
                int position = (row * ROW_SIZE + col); // get the current cell position on the board
                ulong wantedCellMask = 0xfUL << BITS_OF_CELL * position; // create mask of 1 in the position of the wanted cell on the board
                ulong clearWanted = _bitBoard & ~wantedCellMask; // turn off the bits in the wanted position
                ulong newCellToPosition = (ulong)value << BITS_OF_CELL * position; // shift the bits of the new cell to the wanted position
                _bitBoard = clearWanted | newCellToPosition; // add the new cell to the board
            }
        }

        /// <summary>
        /// function that creates a list of all the empty positions on the board and returns it
        /// </summary>
        /// <returns>list of all the empty positions in the board</returns>
        private IList<Position> GetAllEmptyPositions()
        {
            List<Position> emptyPositions = new List<Position>();
            for (int row = 0; row < ROW_SIZE; row++)
            {
                for (int col = 0; col < ROW_SIZE; col++)
                {
                    if (this[row, col] == 0)
                    {
                        emptyPositions.Add(new Position(row, col));
                    }
                }
            }
            return emptyPositions;
        }

        /// <summary>
        /// function that select random empty position from the board
        /// </summary>
        /// <returns>the selected position</returns>
        public Position GetRandomEmptyPosition()
        {
            IList<Position> emptyPositions = GetAllEmptyPositions();
            int randomIndex = random.Next(0, emptyPositions.Count);
            return emptyPositions[randomIndex];
        }

        /// <summary>
        /// function that return the wanted col in type of ushort from down up 
        /// in order to shift this column to dowm
        /// </summary>
        /// <param name="col">the wanted column</param>
        /// <returns>ushort that represent the wanted column from down up</returns>
        private ushort GetColToShiftDown(int col)
        {
            ulong first = (this._bitBoard >> (col + 12) * BITS_OF_CELL) & 0xfL;
            ulong second = (this._bitBoard >> (col + 7) * BITS_OF_CELL) & 0xf0UL;
            ulong third = (this._bitBoard >> (col + 2) * BITS_OF_CELL) & 0xf00UL;
            ulong forth = (this._bitBoard << (3 - col) * BITS_OF_CELL) & 0xf000UL;
            ulong combine = first | second | third | forth;
            return (ushort)combine;
        }

        /// <summary>
        /// function that set to the wanted col a number in type of ushort from down up 
        /// after the colomn was shifted down
        /// </summary>
        /// <param name="col">the wanted column</param>
        /// <param name="shiftedCol">the colomn after shift</param>
        private void SetShiftedDownCol(int col, ushort shiftedCol)
        {
            this._bitBoard =
                    (((_bitBoard ^ (((ulong)(shiftedCol >> 12) & 0xfUL) << (col * BITS_OF_CELL)))
                    ^ (((ulong)(shiftedCol >> 8) & 0xfUl) << (col + 4) * BITS_OF_CELL))
                    ^ (((ulong)(shiftedCol >> 4) & 0xfUl) << (col + 8) * BITS_OF_CELL))
                    ^ (((ulong)(shiftedCol >> 0) & 0xfUl) << (col + 12) * BITS_OF_CELL);
        }

        /// <summary>
        /// function that return the wanted col in type of ushort from up down
        /// in order to shift this column to up
        /// </summary>
        /// <param name="col">the wanted column</param>
        /// <returns>ushort that represent the wanted column from up down</returns>
        private ushort GetColToShiftUp(int col)
        {
            ulong first = (this._bitBoard >> col * BITS_OF_CELL) & 0xfUL;
            ulong second = (this._bitBoard >> (col + 3) * BITS_OF_CELL) & 0xf0UL;
            ulong third = (this._bitBoard >> (col + 6) * BITS_OF_CELL) & 0xf00UL;
            ulong forth = (this._bitBoard >> (col + 9) * BITS_OF_CELL) & 0xf000UL;
            ulong combine = first | second | third | forth;
            return (ushort)combine;
        }

        /// <summary>
        /// function that set to the wanted col a number in type of ushort from up down 
        /// after the colomn was shifted up
        /// </summary>
        /// <param name="col">the wanted column</param>
        /// <param name="shiftedCol">the colomn after shift</param>
        private void SetShiftedUpCol(int col, ushort shiftedCol)
        {
            this._bitBoard =
                (((_bitBoard ^ (((ulong)(shiftedCol >> 0) & 0xfUL) << (col * BITS_OF_CELL)))
                ^ (((ulong)(shiftedCol >> 4) & 0xfUl) << (col + 4) * BITS_OF_CELL))
                ^ (((ulong)(shiftedCol >> 8) & 0xfUl) << (col + 8) * BITS_OF_CELL))
                ^ (((ulong)(shiftedCol >> 12) & 0xfUl) << (col + 12) * BITS_OF_CELL);
        }

        /// <summary>
        /// function that return the wanted row in type of ushort from right left 
        /// in order to shift this column to thr right
        /// </summary>
        /// <param name="row">the wanted row</param>
        /// <returns>ushort that represent the wanted row from right left</returns>
        private ushort GetRowToShiftRight(int row)
        {
            ulong first = this._bitBoard >> ((3 + (row * ROW_SIZE)) * BITS_OF_CELL) & 0xfL;
            ulong second = this._bitBoard >> ((1 + (row * ROW_SIZE)) * BITS_OF_CELL) & 0xf0UL;
            ulong third = ((this._bitBoard >> (1 + (row * ROW_SIZE)) * BITS_OF_CELL) & 0xfUL) << (2 * BITS_OF_CELL);
            ulong forth = (this._bitBoard >> (row * ROW_SIZE * BITS_OF_CELL) & 0xfUL) << (3 * BITS_OF_CELL);
            ulong combine = first | second | third | forth;
            return (ushort)combine;
        }

        /// <summary>
        /// function that set to the wanted row a number in type of ushort from right left 
        /// after the colomn was shifted right
        /// </summary>
        /// <param name="row">the wanted row</param>
        /// <param name="shiftedRow">the colomn after shift</param>
        private void SetShiftedRightRow(int row, ushort shiftedRow)
        {
            this._bitBoard =
                (((_bitBoard ^
                (((ulong)(shiftedRow >> 12) & 0xfUL) << row * ROW_SIZE * BITS_OF_CELL))
                ^ (((ulong)(shiftedRow >> 8) & 0xfUl) << ((1 + row * ROW_SIZE) * BITS_OF_CELL)))
                ^ (((ulong)(shiftedRow >> 4) & 0xfUl) << ((2 + row * ROW_SIZE) * BITS_OF_CELL))
                ^ (((ulong)(shiftedRow >> 0) & 0xfUl) << ((3 + row * ROW_SIZE) * BITS_OF_CELL)));
        }

        /// <summary>
        /// function that return the wanted row in type of ushort from left right 
        /// in order to shift this column to left
        /// </summary>
        /// <param name="row">the wanted row</param>
        /// <returns>ushort that represent the wanted row from left right</returns>
        private ushort GetRowToShiftLeft(int row)
        {
            return (ushort)((this._bitBoard >> ROW_SIZE * BITS_OF_CELL * row) & 0xffffUL);
        }

        /// <summary>
        /// function that set to the wanted row a number in type of ushort from left right 
        /// after the row was shifted down
        /// </summary>
        /// <param name="row">the wanted row</param>
        /// <param name="shiftedRow">the row after shift</param>
        private void SetShiftedLeftRow(int row, ushort shiftedRow)
        {
            this._bitBoard = _bitBoard ^ ((ulong)shiftedRow << row * ROW_SIZE * BITS_OF_CELL);
        }

        /// <summary>
        /// function that receives the wanted direction to move and an index
        /// it returns the row/col of this index by the wanted direction
        /// </summary>
        /// <param name="direction">the wanted direction</param>
        /// <param name="index">the index of the row/col</param>
        /// <returns>ushort that represent the row/col</returns>
        private ushort GetRowOrColByShiftDirection(Direction direction, int index)
        {
            switch (direction)
            {
                case Direction.UP:
                    return GetColToShiftUp(index);
                case Direction.RIGHT:
                    return GetRowToShiftRight(index);
                case Direction.DOWN:
                    return GetColToShiftDown(index);
                case Direction.LEFT:
                    return GetRowToShiftLeft(index);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// function that receives the direction of the move, the row/col after the move and an index
        /// it sets the row/col of this index by the wanted direction
        /// </summary>
        /// <param name="direction">the wanted direction</param>
        /// <param name="index">the index of the row/col</param>
        /// <param name="shifted">the row/col after the shift</param>
        /// <returns>ushort that represent the row/col</returns>
        private void SetShitedRowOrColByDirection(Direction direction, int index, ushort shifted)
        {
            switch (direction)
            {
                case Direction.UP:
                    SetShiftedUpCol(index, shifted);
                    break;
                case Direction.RIGHT:
                    SetShiftedRightRow(index, shifted);
                    break;
                case Direction.DOWN:
                    SetShiftedDownCol(index, shifted);
                    break;
                case Direction.LEFT:
                    SetShiftedLeftRow(index, shifted);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// funtion that receives a direction to move and shifts the board my this direction
        /// and returns true if the move has changen something in the board of false otherwize
        /// </summary>
        /// <param name="direction">the wanted direction to move</param>
        /// <returns>true if the board changed</returns>
        public bool ShiftBoard(Direction direction)
        {
            ushort original;
            ushort shifted;
            bool didShift = false;
            for (int index = 0; index < ROW_SIZE; index++)
            {
                original = GetRowOrColByShiftDirection(direction, index);
                shifted = lookupTable[original].Shifted;
                _emptyCells -= lookupTable[original].EmptyCells;
                _emptyCells += lookupTable[shifted ^ original].EmptyCells;
                _score -= lookupTable[original].Score;
                _score += lookupTable[shifted ^ original].Score;
                _isWon |= lookupTable[original].IsWin;
                if (shifted != 0)
                {
                    didShift = true;
                    SetShitedRowOrColByDirection(direction, index, shifted);
                }
            }
            return didShift;
        }

        /// <summary>
        /// function that returns true if the board is lost
        /// (full and no mergeable cells left)
        /// or false otherwise
        /// </summary>
        /// <returns>true if lost board</returns>
        public bool IsLostBoard()
        {
            if (_emptyCells != 0)
                return false;
            return MergeableCells == 0;
        }

        /// <summary>
        /// property that calculates the number of the mergeable cells in the boars and returns it
        /// </summary>
        public int MergeableCells
        {
            get
            {
                int mergeableCells = 0;
                for (int row = 0; row < ROW_SIZE; row++)
                    mergeableCells += lookupTable[GetRowToShiftLeft(row)].MergeableCells;
                for (int col = 0; col < ROW_SIZE; col++)
                    mergeableCells += lookupTable[GetColToShiftDown(col)].MergeableCells;
                return mergeableCells;
            }
        }

        /// <summary>
        /// property that returns the ulong that represent the board
        /// </summary>
        public ulong BoardKey
        {
            get => _bitBoard;
        }

        /// <summary>
        /// preperty that retuns the number of empty cells in the board
        /// </summary>
        public int EmptyCells
        {
            get => _emptyCells;
            set => _emptyCells = value;
        }

        /// <summary>
        /// property that returns the current score
        /// </summary>
        public uint Score
        {
            get => _score;
        }

        /// <summary>
        /// property that returns if the board is won
        /// </summary>
        public bool IsWon 
        {
            get => _isWon;
        }

        /// <summary>
        /// function that returns a deep copy of the board
        /// </summary>
        /// <returns>deep copy of the board</returns>
        public object Clone()
        {
            BitBoard clone = new BitBoard();
            clone._bitBoard = _bitBoard;
            clone._score = _score;
            clone._emptyCells = _emptyCells;
            clone._isWon = _isWon;
            return clone;
        }

        /// <summary>
        /// no need
        /// </summary>
        /// <returns>the identifier of the object</returns>
        public override int GetHashCode()
        {
            int hashCode = -1515339169;
            hashCode = hashCode * -1521134295 + _bitBoard.GetHashCode();
            hashCode = hashCode * -1521134295 + _score.GetHashCode();
            hashCode = hashCode * -1521134295 + _emptyCells.GetHashCode();
            hashCode = hashCode * -1521134295 + _isWon.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<LookupTable>.Default.GetHashCode(lookupTable);
            hashCode = hashCode * -1521134295 + MergeableCells.GetHashCode();
            hashCode = hashCode * -1521134295 + BoardKey.GetHashCode();
            hashCode = hashCode * -1521134295 + EmptyCells.GetHashCode();
            hashCode = hashCode * -1521134295 + Score.GetHashCode();
            hashCode = hashCode * -1521134295 + IsWon.GetHashCode();
            return hashCode;
        }
    }
}
