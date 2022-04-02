using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    class GameManager
    {

        private BitBoard _board;

        public GameManager()
        {
            InitBoard();
        }

        public void InitBoard()
        {
            _board = new BitBoard();
            for (int i = 0; i < 2; i++)
            {
                AddNewCell();
            }
        }

        public void AddNewCell()
        {
            Position empty = _board.GetRandomEmptyPosition();
            _board[empty.Row, empty.Col] = CellCreator.getValue();
            _board.EmptyCells--;
        }

        public void ShiftBoard(Direction direction)
        {
            bool isShifted = _board.ShiftBoard(direction);
            if (isShifted)
            {
                AddNewCell();
            }
        }

        public uint BoardScore
        {
            get => _board.Score;
        }

        public BitBoard Board
        {
            get => _board;
        }

        public bool IsGameOver()
        {
            return Board.IsLostBoard();    
        }
}
}
