using System;
using System.Collections.Generic;
using System.Drawing;

namespace Game2048.model
{
    class BitBoard
    {

        private const int ROW_SIZE = 4;
        private const int BITS_OF_CELL = 4;
        private static Random random = new Random();

        private ulong _bitBoard; // number that represent the board

        private uint _score;
        private int _emptyCells;
        private bool _isWin;

        private LookupTable lookupTable = LookupTable.Instance;

        public BitBoard()
        {
            this._bitBoard = 0;
            this._score = 0;
            this._isWin = false;
            this._emptyCells = ROW_SIZE * ROW_SIZE;
        }

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

        public Position GetRandomEmptyPosition()
        {
            IList<Position> emptyPositions = GetAllEmptyPositions();
            int randomIndex = random.Next(0, emptyPositions.Count);
            return emptyPositions[randomIndex];
        }

        private ushort GetColToShiftDown(int col)
        {
            ulong first = (this._bitBoard >> (col + 12) * BITS_OF_CELL) & 0xfL;
            ulong second = (this._bitBoard >> (col + 7) * BITS_OF_CELL) & 0xf0UL;
            ulong third = (this._bitBoard >> (col + 2) * BITS_OF_CELL) & 0xf00UL;
            ulong forth = (this._bitBoard << (3 - col) * BITS_OF_CELL) & 0xf000UL;
            ulong combine = first | second | third | forth;
            return (ushort)combine;
        }

        private void SetShiftedDownCol(int col, ushort shiftedCol)
        {
            this._bitBoard =
                    (((_bitBoard ^ (((ulong)(shiftedCol >> 12) & 0xfUL) << (col * BITS_OF_CELL)))
                    ^ (((ulong)(shiftedCol >> 8) & 0xfUl) << (col + 4) * BITS_OF_CELL))
                    ^ (((ulong)(shiftedCol >> 4) & 0xfUl) << (col + 8) * BITS_OF_CELL))
                    ^ (((ulong)(shiftedCol >> 0) & 0xfUl) << (col + 12) * BITS_OF_CELL);
        }

        private ushort GetColToShiftUp(int col)
        {
            ulong first = (this._bitBoard >> col * BITS_OF_CELL) & 0xfUL;
            ulong second = (this._bitBoard >> (col + 3) * BITS_OF_CELL) & 0xf0UL;
            ulong third = (this._bitBoard >> (col + 6) * BITS_OF_CELL) & 0xf00UL;
            ulong forth = (this._bitBoard >> (col + 9) * BITS_OF_CELL) & 0xf000UL;
            ulong combine = first | second | third | forth;
            return (ushort)combine;
        }

        private void SetShiftedUpCol(int col, ushort shiftedCol)
        {
            this._bitBoard =
                (((_bitBoard ^ (((ulong)(shiftedCol >> 0) & 0xfUL) << (col * BITS_OF_CELL)))
                ^ (((ulong)(shiftedCol >> 4) & 0xfUl) << (col + 4) * BITS_OF_CELL))
                ^ (((ulong)(shiftedCol >> 8) & 0xfUl) << (col + 8) * BITS_OF_CELL))
                ^ (((ulong)(shiftedCol >> 12) & 0xfUl) << (col + 12) * BITS_OF_CELL);
        }

        private ushort GetRowToShiftRight(int row)
        {
            ulong first = this._bitBoard >> ((3 + (row * ROW_SIZE)) * BITS_OF_CELL) & 0xfL;
            ulong second = this._bitBoard >> ((1 + (row * ROW_SIZE)) * BITS_OF_CELL) & 0xf0UL;
            ulong third = ((this._bitBoard >> (1 + (row * ROW_SIZE)) * BITS_OF_CELL) & 0xfUL) << (2 * BITS_OF_CELL);
            ulong forth = (this._bitBoard >> (row * ROW_SIZE * BITS_OF_CELL) & 0xfUL) << (3 * BITS_OF_CELL);
            ulong combine = first | second | third | forth;
            return (ushort)combine;
        }

        private void SetShiftedRightRow(int row, ushort shiftedRow)
        {
            this._bitBoard = 
                (((_bitBoard ^
                (((ulong)(shiftedRow >> 12) & 0xfUL) << row * ROW_SIZE * BITS_OF_CELL))
                ^ (((ulong)(shiftedRow >> 8) & 0xfUl) << ((1 + row * ROW_SIZE) * BITS_OF_CELL)))
                ^ (((ulong)(shiftedRow >> 4) & 0xfUl) << ((2 + row * ROW_SIZE) * BITS_OF_CELL))
                ^ (((ulong)(shiftedRow >> 0) & 0xfUl) << ((3 + row * ROW_SIZE) * BITS_OF_CELL)));
        }

        private ushort GetRowToShiftLeft(int row)
        {
            return (ushort)((this._bitBoard >> ROW_SIZE * BITS_OF_CELL * row) & 0xffffUL);
        }

        private void SetShiftedLeftRow(int row, ushort shiftedRow)
        {
            this._bitBoard = _bitBoard ^ ((ulong)shiftedRow << row * ROW_SIZE * BITS_OF_CELL);
        }

        private ushort GetRowOrColByShiftDirection(Direction direction, int index)
        {
            switch (direction)
            {
                case Direction.UP:
                    return GetColToShiftUp(index);
                case Direction.RIGTH:
                    return GetRowToShiftRight(index);
                case Direction.DOWN:
                    return GetColToShiftDown(index);
                case Direction.LEFT:
                    return GetRowToShiftLeft(index);
                default:
                    return 0;
            }
        }

        private void SetShitedRowOrColByDirection(Direction direction, int index, ushort shifted)
        {
            switch (direction)
            {
                case Direction.UP:
                    SetShiftedUpCol(index, shifted);
                    break;
                case Direction.RIGTH:
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
                _isWin |= lookupTable[original].IsWin;
                if (shifted != 0)
                {
                    didShift = true;
                    SetShitedRowOrColByDirection(direction, index, shifted);
                }
            }
            return didShift;
        }

        public int EmptyCells
        {
            get => _emptyCells;
            set => _emptyCells = value;
        }

        public override bool Equals(object obj)
        {
            return obj is BitBoard board &&
                   _bitBoard == board._bitBoard;
        }
    }
}
