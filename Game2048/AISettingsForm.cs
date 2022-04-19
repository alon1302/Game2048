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
    public partial class AISettingsForm : Form
    {
        int depth = 3; // the wanted AI search depth

        /// <summary>
        /// constructor that creates new AI settings form
        /// </summary>
        public AISettingsForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// auto function no use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// auto function for the click event on the minus for search depth
        /// it subs one the the search depth in it is above 2
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void PBminus_Click(object sender, EventArgs e)
        {
            if (depth > 2)
            {
                depth--;
                label3.Text = depth.ToString();
            }
        }

        /// <summary>
        /// auto function for the click event on the plus for search depth
        /// it adds one the the search depth in it is less than 4
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void PBplus_Click(object sender, EventArgs e)
        {
            if (depth < 4)
            {
                depth++;
                label3.Text = depth.ToString();
            }
        }

        /// <summary>
        /// auto function for the click event on the start game "button"
        /// the function creates new game form with the chosen AI settings
        /// make the current form hide and the new one show
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void label7_Click(object sender, EventArgs e)
        {
            GameForm gameForm;
            if (rbCorner.Checked)
                gameForm = new GameForm(depth, AIStrategy.CORNER);
            else
                gameForm = new GameForm(depth, AIStrategy.SNAKE);
            this.Hide();
            gameForm.Show();
        }
    }
}
