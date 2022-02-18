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
                    MessageBox.Show("pressed right arrow");
                    break;
                case Keys.Left:
                    MessageBox.Show("pressed left arrow");
                    break;
                case Keys.Up:
                    MessageBox.Show("pressed up arrow");
                    break;
                case Keys.Down:
                    MessageBox.Show("pressed down arrow");
                    break;
                default:
                    return;
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
