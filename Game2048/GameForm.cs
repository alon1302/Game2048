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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                    gameManager.ShiftBoard(Direction.RIGTH);
                    break;
                case Keys.Left:
                    gameManager.ShiftBoard(Direction.LEFT);
                    break;
                case Keys.Up:
                    gameManager.ShiftBoard(Direction.UP);
                    break;
                case Keys.Down:
                    gameManager.ShiftBoard(Direction.DOWN);
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);

            }
            UpdateUI();
            if (gameManager.IsGameOver())
            {
                MessageBox.Show("game over");
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        private void button1_Click(object sender, EventArgs e)
        {
            gameManager = new GameManager();
            UpdateUI();
        }

        private void UpdateUI()
        {
            pictureBox1.Invalidate();
            label1.Text = gameManager.BoardScore.ToString();
        }
    }
}
