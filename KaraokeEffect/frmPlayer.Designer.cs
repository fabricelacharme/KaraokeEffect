namespace KaraokeEffect
{
    partial class frmPlayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlayer));
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.karaokeEffect1 = new lyrics.KaraokeEffect();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 282);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 100);
            this.panel1.TabIndex = 1;
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1000;
            this.trackBar1.Location = new System.Drawing.Point(12, 6);
            this.trackBar1.Maximum = 10000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(665, 45);
            this.trackBar1.SmallChange = 1000;
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickFrequency = 500;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(353, 57);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Stop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(166, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Pause";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // karaokeEffect1
            // 
            this.karaokeEffect1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.karaokeEffect1.KaraokeFont = new System.Drawing.Font("Comic Sans MS", 56F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.karaokeEffect1.Location = new System.Drawing.Point(0, 0);
            this.karaokeEffect1.Name = "karaokeEffect1";
            this.karaokeEffect1.nbLyricsLines = 3;
            this.karaokeEffect1.Position = 0;
            this.karaokeEffect1.Size = new System.Drawing.Size(689, 282);
            this.karaokeEffect1.StepPercent = 0.01F;
            this.karaokeEffect1.TabIndex = 2;
            this.karaokeEffect1.TimerInterval = 10;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 382);
            this.Controls.Add(this.karaokeEffect1);
            this.Controls.Add(this.panel1);
            this.Name = "frmPlayer";
            this.Text = "frmPlayer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private lyrics.KaraokeEffect karaokeEffect1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}