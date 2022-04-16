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
    public partial class OpenForm : Form
    {
        Timer rotation;
        PictureBox currentFlip;

        public OpenForm()
        {
            InitializeComponent();
            rotation = new Timer();
            rotation.Interval = 50;
            rotation.Tick += rotationTimer_Tick;
            AddEventHandlers();
        }

        private void rotationTimer_Tick(object sender, EventArgs e)
        {
            Image flip = currentFlip.Image;
            flip.RotateFlip(RotateFlipType.Rotate90FlipXY);
            currentFlip.Image = flip;
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            currentFlip = (PictureBox)sender;
            rotation.Start();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            rotation.Stop();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            this.Hide();
            gameForm.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AISettingsForm settingsForm = new AISettingsForm();
            this.Hide();
            settingsForm.Show();
        }

        private void AddEventHandlers()
        {
            pictureBox1.MouseEnter += pictureBox_MouseEnter;
            pictureBox1.MouseLeave += pictureBox_MouseLeave;
            pictureBox2.MouseEnter += pictureBox_MouseEnter;
            pictureBox2.MouseLeave += pictureBox_MouseLeave;
            pictureBox3.MouseEnter += pictureBox_MouseEnter;
            pictureBox3.MouseLeave += pictureBox_MouseLeave;
            pictureBox4.MouseEnter += pictureBox_MouseEnter;
            pictureBox4.MouseLeave += pictureBox_MouseLeave;
            pictureBox5.MouseEnter += pictureBox_MouseEnter;
            pictureBox5.MouseLeave += pictureBox_MouseLeave;
            pictureBox6.MouseEnter += pictureBox_MouseEnter;
            pictureBox6.MouseLeave += pictureBox_MouseLeave;
            pictureBox7.MouseEnter += pictureBox_MouseEnter;
            pictureBox7.MouseLeave += pictureBox_MouseLeave;
            pictureBox8.MouseEnter += pictureBox_MouseEnter;
            pictureBox8.MouseLeave += pictureBox_MouseLeave;
            pictureBox9.MouseEnter += pictureBox_MouseEnter;
            pictureBox9.MouseLeave += pictureBox_MouseLeave;
            pictureBox10.MouseEnter += pictureBox_MouseEnter;
            pictureBox10.MouseLeave += pictureBox_MouseLeave;
            pictureBox11.MouseEnter += pictureBox_MouseEnter;
            pictureBox11.MouseLeave += pictureBox_MouseLeave;
            pictureBox12.MouseEnter += pictureBox_MouseEnter;
            pictureBox12.MouseLeave += pictureBox_MouseLeave;
        }


    }
}
