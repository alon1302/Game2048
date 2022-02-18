using Game2048.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game2048
{

    public partial class GameForm : Form
    {
        private BitBoard board;
        public GameForm()
        {
            board = new BitBoard();
            InitializeComponent();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    board.ShiftBoard(Direction.RIGTH);
                    MessageBox.Show("pressed right arrow");
                    break;
                case Keys.Left:
                    board.ShiftBoard(Direction.LEFT);
                    MessageBox.Show("pressed left arrow");
                    break;
                case Keys.Up:
                    board.ShiftBoard(Direction.UP);
                    MessageBox.Show("pressed up arrow");
                    break;
                case Keys.Down:
                    board.ShiftBoard(Direction.DOWN);
                    MessageBox.Show("pressed down arrow");
                    break;
                default:
                    return;
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            board.Paint(e.Graphics);
        }
    }
}
