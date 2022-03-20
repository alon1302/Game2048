using Game2048.model;
using Game2048.view;
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
            MakeLabelTransparent();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    gameManager.ShiftBoard(Direction.RIGTH);
                    //MessageBox.Show("pressed right arrow");
                    break;
                case Keys.Left:
                    gameManager.ShiftBoard(Direction.LEFT);
                    //MessageBox.Show("pressed left arrow");
                    break;
                case Keys.Up:
                    gameManager.ShiftBoard(Direction.UP);
                    //MessageBox.Show("pressed up arrow");
                    break;
                case Keys.Down:
                    gameManager.ShiftBoard(Direction.DOWN);
                    //MessageBox.Show("pressed down arrow");
                    break;
                default:
                    return;
            }
            pictureBox1.Invalidate();
            label1.Text = gameManager.BoardScore.ToString();
            if (gameManager.IsGameOver())
            {
                MessageBox.Show("game over");
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsManager.PaintBoard(e.Graphics, gameManager.Board);
        }

        private void MakeLabelTransparent()
        {
            //Point pos = this.PointToScreen(label1.Location);
            //pos = pictureBox1.PointToClient(pos);
            //label1.Parent = pictureBox1;
            //label1.Location = pos;
            //label1.BackColor = Color.Transparent;
        }
    }
}
