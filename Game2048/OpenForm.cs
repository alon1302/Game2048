using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game2048
{
    public partial class OpenForm : Form
    {
        Timer timer; // timer for the images flip
        PictureBox currentFlip; // the current image that the mouse on

        /// <summary>
        /// constructor that creats new form and set the settings for the timer 
        /// </summary>
        public OpenForm()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += rotationTimer_Tick;
            AddEventHandlers();
        }

        /// <summary>
        /// auto function for the tick event of the timer
        /// the function flip the current image by 90 degrees
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void rotationTimer_Tick(object sender, EventArgs e)
        {
            Image flip = currentFlip.Image;
            flip.RotateFlip(RotateFlipType.Rotate90FlipXY);
            currentFlip.Image = flip;
        }

        /// <summary>
        /// auto function for the mouse enter event of all the picturebox in this form
        /// the function sets the current picture and starts the timer 
        /// </summary>
        /// <param name="sender">auto parameter that represent the picture</param>
        /// <param name="e">auto parameter no use</param>
        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            currentFlip = (PictureBox)sender;
            timer.Start();
        }

        /// <summary>
        /// auto function for the mouse leave event of all the picturebox in this form
        /// the function stops the timer
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            timer.Stop();
        }

        /// <summary>
        /// auto function fot the click event on the single player "button"
        /// the function creates new game form, make the current form hide and make the new form show 
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void label3_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm();
            this.Hide();
            gameForm.Show();
        }

        /// <summary>
        /// auto function fot the click event on the AI player "button"
        /// the function creates new AI settings form, make the current form hide and make the new form show 
        /// </summary>
        /// <param name="sender">auto parameter no use</param>
        /// <param name="e">auto parameter no use</param>
        private void label4_Click(object sender, EventArgs e)
        {
            AISettingsForm settingsForm = new AISettingsForm();
            this.Hide();
            settingsForm.Show();
        }

        /// <summary>
        /// function that add the mouse enter and leave events for all the picture boxes
        /// </summary>
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
