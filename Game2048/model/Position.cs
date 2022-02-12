using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.model
{
    class Position
    {
        private int row; // row index
        private int col; // column index

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
        public int Col
        {
            get { return col;}
            set { col = value;}
        }
        public int Row
        {
            get { return row; }
            set { row = value; }
        }
    }
}
