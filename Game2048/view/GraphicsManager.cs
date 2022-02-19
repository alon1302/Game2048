using Game2048.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.view
{
    class GraphicsManager
    {
        private const int ROW_SIZE = 4;
        private const int WALL_SIZE = 13;
        private const int CELL_SIZE = 107;

        private static Dictionary<int, Image> valueToImage = new Dictionary<int, Image>()
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

        public static void PaintBoard(Graphics graphics, BitBoard board)
        {
            Image image;
            for (int i = 0; i < ROW_SIZE; i++)
            {
                for (int j = 0; j < ROW_SIZE; j++)
                {
                    int currentCell = board[i, j];
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
        public static Image getImageFromValue(int value)
        {
            return valueToImage[value];
        }
    }
}
