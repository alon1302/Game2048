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
        private const int ROW_SIZE = 4;
        private const int WALL_SIZE = 16;
        private const int CELL_SIZE = 103;

        private Dictionary<int, Image> valueToImage = new Dictionary<int, Image>()
        {
            {1,Properties.Resources._2},
            {2,Properties.Resources._4},
            {3,Properties.Resources._8},
            {4,Properties.Resources._16},
            {5,Properties.Resources._32},
            {6,Properties.Resources._64},
            {7,Properties.Resources._128},
            {8,Properties.Resources._256},
            {9,Properties.Resources._512},
            {10,Properties.Resources._1024},
            {11,Properties.Resources._2048},
            {12,Properties.Resources._4096}
        };

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
            _board.ShiftBoard(direction);
            AddNewCell();
        }


        public void PaintBoard(Graphics graphics)
        {
            Image image;
            for (int i = 0; i < ROW_SIZE; i++)
            {
                for (int j = 0; j < ROW_SIZE; j++)
                {
                    int currentCell = _board[i, j];
                    if (currentCell != 0)
                    {
                        image = getImageFromValue(currentCell);
                        graphics.DrawImage(image,
                            (j + 1) * WALL_SIZE + j * CELL_SIZE,
                            (i + 1) * WALL_SIZE + i * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                    }
                }
            }
        }

        public Image getImageFromValue(int value)
        {
            return valueToImage[value];
        }

    }
}
