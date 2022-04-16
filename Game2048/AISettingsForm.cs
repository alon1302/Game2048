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
        int depth = 3;

        public AISettingsForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void PBminus_Click(object sender, EventArgs e)
        {
            if (depth > 2)
            {
                depth--;
                label3.Text = depth.ToString();
            }
        }

        private void rbCorner_CheckedChanged(object sender, EventArgs e)
        {

        }

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

        private void PBplus_Click(object sender, EventArgs e)
        {
            if (depth < 4)
            {
                depth++;
                label3.Text = depth.ToString();
            }
        }
    }
}
