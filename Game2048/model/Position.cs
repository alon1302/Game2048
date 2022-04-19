using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    /// <summary>
    /// class that represent a cell location in the board
    /// </summary>
    class Position
    {
        private int row; // row index
        private int col; // column index

        /// <summary>
        /// constructor that creates object of this class and initiates it with row and col
        /// </summary>
        /// <param name="row">row of cell</param>
        /// <param name="col">column of cell</param>
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// property that returns instance to the column of the cell 
        /// to get or set its value
        /// </summary>
        public int Col
        {
            get { return col;}
            set { col = value;}
        }

        /// <summary>
        /// property that returns instance to the row of the cell 
        /// to get or set its value
        /// </summary>
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
    }
}
