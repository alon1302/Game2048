
namespace Game2048
{
    partial class AISettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AISettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PBplus = new System.Windows.Forms.PictureBox();
            this.PBminus = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rbCorner = new System.Windows.Forms.RadioButton();
            this.rbSnake = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PBplus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBminus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(115, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "AI Settings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(145, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search Depth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(230, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 31);
            this.label3.TabIndex = 2;
            this.label3.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F);
            this.label4.Location = new System.Drawing.Point(180, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 35);
            this.label4.TabIndex = 5;
            this.label4.Text = "Strategy";
            // 
            // PBplus
            // 
            this.PBplus.Image = ((System.Drawing.Image)(resources.GetObject("PBplus.Image")));
            this.PBplus.Location = new System.Drawing.Point(265, 171);
            this.PBplus.Name = "PBplus";
            this.PBplus.Size = new System.Drawing.Size(30, 30);
            this.PBplus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBplus.TabIndex = 4;
            this.PBplus.TabStop = false;
            this.PBplus.Click += new System.EventHandler(this.PBplus_Click);
            // 
            // PBminus
            // 
            this.PBminus.Image = ((System.Drawing.Image)(resources.GetObject("PBminus.Image")));
            this.PBminus.Location = new System.Drawing.Point(195, 171);
            this.PBminus.Name = "PBminus";
            this.PBminus.Size = new System.Drawing.Size(30, 30);
            this.PBminus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBminus.TabIndex = 3;
            this.PBminus.TabStop = false;
            this.PBminus.Click += new System.EventHandler(this.PBminus_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Game2048.Properties.Resources.snake;
            this.pictureBox1.Location = new System.Drawing.Point(41, 335);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(190, 190);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label5.Location = new System.Drawing.Point(106, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "Snake";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Game2048.Properties.Resources.corner;
            this.pictureBox2.Location = new System.Drawing.Point(271, 335);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(190, 190);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label6.Location = new System.Drawing.Point(330, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 24);
            this.label6.TabIndex = 9;
            this.label6.Text = "Corner";
            // 
            // rbCorner
            // 
            this.rbCorner.AutoSize = true;
            this.rbCorner.Location = new System.Drawing.Point(360, 531);
            this.rbCorner.Name = "rbCorner";
            this.rbCorner.Size = new System.Drawing.Size(14, 13);
            this.rbCorner.TabIndex = 10;
            this.rbCorner.TabStop = true;
            this.rbCorner.UseVisualStyleBackColor = true;
            // 
            // rbSnake
            // 
            this.rbSnake.AutoSize = true;
            this.rbSnake.Location = new System.Drawing.Point(130, 531);
            this.rbSnake.Name = "rbSnake";
            this.rbSnake.Size = new System.Drawing.Size(14, 13);
            this.rbSnake.TabIndex = 11;
            this.rbSnake.TabStop = true;
            this.rbSnake.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(195, 557);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 110);
            this.label7.TabIndex = 12;
            this.label7.Text = "Start Game";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // AISettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 692);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rbSnake);
            this.Controls.Add(this.rbCorner);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PBplus);
            this.Controls.Add(this.PBminus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AISettingsForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PBplus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBminus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox PBminus;
        private System.Windows.Forms.PictureBox PBplus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbCorner;
        private System.Windows.Forms.RadioButton rbSnake;
        private System.Windows.Forms.Label label7;
    }
}