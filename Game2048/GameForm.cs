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
        private GameManager gameManager;
        public GameForm()
        {
            gameManager = new GameManager();
            InitializeComponent();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    gameManager.ShiftBoard(Direction.RIGTH);
                    pictureBox1.Invalidate();
                    //MessageBox.Show("pressed right arrow");
                    break;
                case Keys.Left:
                    gameManager.ShiftBoard(Direction.LEFT);
                    pictureBox1.Invalidate();
                    //MessageBox.Show("pressed left arrow");
                    break;
                case Keys.Up:
                    gameManager.ShiftBoard(Direction.UP);
                    pictureBox1.Invalidate();
                    //MessageBox.Show("pressed up arrow");
                    break;
                case Keys.Down:
                    gameManager.ShiftBoard(Direction.DOWN);
                    pictureBox1.Invalidate();
                    //MessageBox.Show("pressed down arrow");
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
            gameManager.PaintBoard(e.Graphics);
        }
    }
}
