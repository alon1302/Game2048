using Game2048.model;
using Game2048.view;
using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace Game2048
{
    public partial class GameForm : Form
    {
        private GameManager gameManager; // instance of game manager
        private AIManager AIManager; // instance of AI manager

        private delegate void DELEGATE();
        private Thread gameAIThread;
        bool game;

        /// <summary>
        /// constructor that creates form for user play
        /// </summary>
        public GameForm()
        {
            gameManager = new GameManager();
            InitializeComponent();
        }

        /// <summary>
        /// constructor that receives the settings of the AI and creates new form for AI play
        /// </summary>
        /// <param name="depth">the maximum depth for the AI</param>
        /// <param name="strategy">the chosen strategy for the AI</param>
        public GameForm(int depth, AIStrategy strategy) : this()
        {
            AIManager = new AIManager(depth, strategy);
            StartNewAIGame();
        }

        private void StartNewAIGame()
        {
            gameAIThread = new Thread(new ThreadStart(runAIGame));
            gameAIThread.Start();
        }

        /// <summary>
        /// function that runs the game for the AI
        /// select move and make it until lose
        /// </summary>
        public void runAIGame()
        {
            DELEGATE updateUIDelegate = new DELEGATE(UpdateUI);
            game = true;
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
                    this.Invoke(updateUIDelegate);
                    Thread.Sleep(100);
                }
            }
            Console.WriteLine("FINISH");
        }

        /// <summary>
        /// function that listens to the keyboard press by the user
        /// </summary>
        /// <param name="msg">auto parameter have no use</param>
        /// <param name="keyData">the key that has pressed</param>
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// auto function have no use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// auto function for the paint event 
        /// the function calls to a function that paints the board
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter to get the graphics object from</param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsManager.PaintBoard(e.Graphics, gameManager.Board); 
        }

        /// <summary>
        /// function that update the view for the user
        /// paint the board set the score and check for lose to alert the user
        /// </summary>
        private void UpdateUI()
        {
            pictureBox1.Refresh();
            label1.Text = gameManager.BoardScore.ToString();
            label1.Refresh();
            if (gameManager.IsGameLost())
            {
                MessageBox.Show("game over"); // TODO: end game screen
            }
        }

        /// <summary>
        /// auto function for the click event on the restart button
        /// the function restarts the game 
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void picturebox_restart_Click(object sender, EventArgs e)
        {
            gameManager = new GameManager();
            UpdateUI();
            if (AIManager != null)
            {
                gameAIThread.Abort();
                StartNewAIGame();
            }
        }

        /// <summary>
        /// auto function for the click event on the home button
        /// the function restarts the game 
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void pictureBox_home_Click(object sender, EventArgs e)
        {
            if (AIManager != null)
                gameAIThread.Abort();
            OpenForm openForm = new OpenForm();
            openForm.Show();
            this.Hide();
        }
    }
}
