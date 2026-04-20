namespace Particle_system
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            picDisplay = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            tbDirection = new TrackBar();
            lblDirection = new Label();
            points = new Label();
            btnTower = new Button();
            btnHP = new Button();
            ((System.ComponentModel.ISupportInitialize)picDisplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbDirection).BeginInit();
            SuspendLayout();
            // 
            // picDisplay
            // 
            picDisplay.Location = new Point(12, 12);
            picDisplay.Name = "picDisplay";
            picDisplay.Size = new Size(810, 530);
            picDisplay.TabIndex = 0;
            picDisplay.TabStop = false;
            picDisplay.MouseClick += picDisplay_MouseClick;
            picDisplay.MouseMove += picDisplay_MouseMove;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 40;
            timer1.Tick += timer1_Tick;
            // 
            // tbDirection
            // 
            tbDirection.Location = new Point(12, 548);
            tbDirection.Maximum = 359;
            tbDirection.Name = "tbDirection";
            tbDirection.Size = new Size(155, 45);
            tbDirection.TabIndex = 1;
            tbDirection.Scroll += tbDirection_Scroll;
            // 
            // lblDirection
            // 
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(173, 560);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(0, 15);
            lblDirection.TabIndex = 2;
            // 
            // points
            // 
            points.AutoSize = true;
            points.Location = new Point(758, 560);
            points.Name = "points";
            points.Size = new Size(0, 15);
            points.TabIndex = 5;
            // 
            // btnTower
            // 
            btnTower.Location = new Point(254, 556);
            btnTower.Name = "btnTower";
            btnTower.Size = new Size(93, 23);
            btnTower.TabIndex = 6;
            btnTower.Text = "Башня = 50";
            btnTower.UseVisualStyleBackColor = true;
            btnTower.Click += btnTower_Click;
            // 
            // btnHP
            // 
            btnHP.Location = new Point(353, 556);
            btnHP.Name = "btnHP";
            btnHP.Size = new Size(89, 23);
            btnHP.TabIndex = 7;
            btnHP.Text = "+50 HP (25)";
            btnHP.UseVisualStyleBackColor = true;
            btnHP.Click += btnHP_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 597);
            Controls.Add(btnHP);
            Controls.Add(btnTower);
            Controls.Add(points);
            Controls.Add(lblDirection);
            Controls.Add(tbDirection);
            Controls.Add(picDisplay);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)picDisplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbDirection).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picDisplay;
        private System.Windows.Forms.Timer timer1;
        private TrackBar tbDirection;
        private Label lblDirection;
        private Label points;
        private Button btnTower;
        private Button btnHP;
    }
}
