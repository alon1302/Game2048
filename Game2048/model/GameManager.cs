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
            _board[empty.Row, empty.Col] = NewCellGenerator.getValue();
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
            Image image;
            switch (value)
            {
                case 1:
                    image = Properties.Resources._2;
                    break;
                case 2:
                    image = Properties.Resources._4;
                    break;
                case 3:
                    image = Properties.Resources._8;
                    break;
                case 4:
                    image = Properties.Resources._16;
                    break;
                case 5:
                    image = Properties.Resources._32;
                    break;
                case 6:
                    image = Properties.Resources._64;
                    break;
                case 7:
                    image = Properties.Resources._128;
                    break;
                case 8:
                    image = Properties.Resources._256;
                    break;
                case 9:
                    image = Properties.Resources._512;
                    break;
                case 10:
                    image = Properties.Resources._1024;
                    break;
                case 11:
                    image = Properties.Resources._2048;
                    break;
                case 12:
                    image = Properties.Resources._4096;
                    break;
                //case 13:
                //    image = Properties.Resources._1024;
                //    break;
                default:
                    image = null;
                    break;
            }
            return image;
        }

    }
}
