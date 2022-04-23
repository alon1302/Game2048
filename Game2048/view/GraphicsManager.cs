using Game2048.model;
using System.Collections.Generic;
using System.Drawing;

namespace Game2048.view
{
    class GraphicsManager
    {
        private const int WALL_SIZE = 13; // size of the walls in the graphic board 
        private const int CELL_SIZE = 107; // size of the cell in the graphic board

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
        }; // dictionary that works as value to image map

        /// <summary>
        /// function that receives graphics and board
        /// and paint the board on this graphics
        /// </summary>
        /// <param name="graphics">graphics instance</param>
        /// <param name="board">board to paint</param>
        public static void PaintBoard(Graphics graphics, BitBoard board)
        {
            Image image;
            for (int i = 0; i < BitBoard.ROW_SIZE; i++)
            {
                for (int j = 0; j < BitBoard.ROW_SIZE; j++)
                {
                    int currentCell = board[i, j];
                    if (currentCell != 0)
                    {
                        image = valueToImage[currentCell];
                        graphics.DrawImage(image,
                            (j + 1) * WALL_SIZE + j * CELL_SIZE,
                            (i + 1) * WALL_SIZE + i * CELL_SIZE, CELL_SIZE, CELL_SIZE);
                    }
                }
            }
        }
    }
}
