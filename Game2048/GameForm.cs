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

        bool firstClick = true;
        public GameForm(int depth, AIStrategy strategy)
        {
            gameManager = new GameManager();
            InitializeComponent();
            AIManager = new AIManager(depth, strategy);                
        }

        public GameForm()
        {
            gameManager = new GameManager();
            InitializeComponent();
        }

        public void runAIGame()
        {
            bool game = true;
            Direction currentMove;
            while (game)
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
                    Thread.Sleep(100);
                }
            }
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
            else
            {
                if (keyData == Keys.Enter)
                {           
                    if (firstClick)
                    {
                        runAIGame();
                        firstClick = false;
                    }
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            gameManager = new GameManager();
            UpdateUI();
        }

        private void UpdateUI()
        {
            pictureBox1.Refresh();
            label1.Text = gameManager.BoardScore.ToString();
            label1.Refresh();
            if (gameManager.IsGameOver())
            {
                MessageBox.Show("game over"); // TODO: end game screen
            }
        }
    }
}
