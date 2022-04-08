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
using System.Threading;

namespace Game2048
{

    public enum GameMode
    {
        NORMAL,
        AI
    }

    public partial class GameForm : Form
    {

        private const int AI_DEPTH = 3;

        private GameManager gameManager;
        private AIManager AIManager; 
        public GameForm(GameMode mode)
        {
            gameManager = new GameManager();          
            InitializeComponent();
            if (mode == GameMode.AI)
            {
                AIManager = new AIManager(AI_DEPTH);
            }
        }

        public void runAIGame()
        {
            bool game = true;
            Direction currentMove;
            do
            {
                currentMove = AIManager.GetBestMove(gameManager.Board);
                if (currentMove == Direction.NONE)
                {
                    game = false;
                }
                else
                {
                    gameManager.ShiftBoard(currentMove);
                    UpdateUI();
                    Thread.Sleep(500);
                }
            } while (game);
            Console.WriteLine("FINISH");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (AIManager == null)
            {
                switch (keyData)
                {
                    case Keys.Right:
                        gameManager.ShiftBoard(Direction.RIGHT);
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
            }
            //else
            //{
            //    if (keyData == Keys.Enter)
            //    {
            //        runAIGame();
            //    }
            //}
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //if (AIManager != null)
            //{
            //    //runAIGame();
            //}
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            runAIGame();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsManager.PaintBoard(e.Graphics, gameManager.Board);
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
            if (gameManager.IsGameOver())
            {
                MessageBox.Show("game over"); // TODO: end game screen
            }
        }
    }
}
