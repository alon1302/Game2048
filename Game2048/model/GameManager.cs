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
        private BitBoard _board; // reference of the current board

        /// <summary>
        /// constructor that creates a new game manager
        /// </summary>
        public GameManager()
        {
            InitBoard();
        }

        /// <summary>
        /// function that initialize a new board and place 2 cells randomly 
        /// </summary>
        public void InitBoard()
        {
            _board = new BitBoard();
            for (int i = 0; i < 2; i++)
            {
                AddNewCell();
            }
        }

        /// <summary>
        /// function that add a new cell randomly to the board
        /// </summary>
        public void AddNewCell()
        {
            Position empty = _board.GetRandomEmptyPosition();
            _board[empty.Row, empty.Col] = CellCreator.getValue();
            _board.EmptyCells--;
        }

        /// <summary>
        /// function that receives a direction of a move and shift the board by this direction
        /// if the board has changed calls the function that add a new cell
        /// </summary>
        /// <param name="direction">move direction</param>
        public void ShiftBoard(Direction direction)
        {
            bool isShifted = _board.ShiftBoard(direction);
            if (isShifted)
            {
                AddNewCell();
            }
        }

        /// <summary>
        /// property that returns the score of the current game
        /// </summary>
        public uint BoardScore
        {
            get => _board.Score;
        }

        /// <summary>
        /// function that returns a reference to the current board
        /// </summary>
        public BitBoard Board
        {
            get => _board;
        }

        /// <summary>
        /// function that return true if the game is over
        /// </summary>
        /// <returns></returns>
        public bool IsGameLost()
        {
            return Board.IsLostBoard();
        }
    }
}
