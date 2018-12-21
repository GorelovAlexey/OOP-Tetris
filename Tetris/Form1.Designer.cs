namespace Tetris
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelGameEnded = new System.Windows.Forms.Label();
            this.pauseLabel = new System.Windows.Forms.Label();
            this.pictureBoxMain = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxNextFigure = new System.Windows.Forms.PictureBox();
            this.labelDifficulty = new System.Windows.Forms.Label();
            this.labelScoreBoard = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelExit = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelHowToPlay = new System.Windows.Forms.Label();
            this.labelMusic = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNextFigure)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.labelGameEnded);
            this.panel1.Controls.Add(this.pauseLabel);
            this.panel1.Controls.Add(this.pictureBoxMain);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(2, 26);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(596, 769);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // labelGameEnded
            // 
            this.labelGameEnded.AutoSize = true;
            this.labelGameEnded.Font = new System.Drawing.Font("Microsoft Sans Serif", 35.25F);
            this.labelGameEnded.ForeColor = System.Drawing.Color.DeepPink;
            this.labelGameEnded.Location = new System.Drawing.Point(34, 305);
            this.labelGameEnded.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelGameEnded.Name = "labelGameEnded";
            this.labelGameEnded.Size = new System.Drawing.Size(382, 108);
            this.labelGameEnded.TabIndex = 23;
            this.labelGameEnded.Text = "game ended press \'r\' to restart";
            this.labelGameEnded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pauseLabel
            // 
            this.pauseLabel.BackColor = System.Drawing.Color.Transparent;
            this.pauseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pauseLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.pauseLabel.Location = new System.Drawing.Point(169, 51);
            this.pauseLabel.Name = "pauseLabel";
            this.pauseLabel.Size = new System.Drawing.Size(111, 660);
            this.pauseLabel.TabIndex = 22;
            this.pauseLabel.Text = "PAUSED";
            this.pauseLabel.Visible = false;
            // 
            // pictureBoxMain
            // 
            this.pictureBoxMain.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxMain.Location = new System.Drawing.Point(16, 2);
            this.pictureBoxMain.Name = "pictureBoxMain";
            this.pictureBoxMain.Size = new System.Drawing.Size(426, 761);
            this.pictureBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMain.TabIndex = 21;
            this.pictureBoxMain.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Blue;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(-91, -1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 37);
            this.label7.TabIndex = 6;
            this.label7.Text = " ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.76958F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelScore, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxNextFigure, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelDifficulty, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelScoreBoard, 0, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(448, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 332F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(142, 755);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(5, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 51);
            this.label5.TabIndex = 3;
            this.label5.Text = "Next:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelScore.ForeColor = System.Drawing.Color.Silver;
            this.labelScore.Location = new System.Drawing.Point(5, 227);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(132, 119);
            this.labelScore.TabIndex = 1;
            this.labelScore.Text = "00000";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(5, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 64);
            this.label2.TabIndex = 0;
            this.label2.Text = "Score:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxNextFigure
            // 
            this.pictureBoxNextFigure.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxNextFigure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxNextFigure.Location = new System.Drawing.Point(5, 56);
            this.pictureBoxNextFigure.Name = "pictureBoxNextFigure";
            this.pictureBoxNextFigure.Size = new System.Drawing.Size(132, 104);
            this.pictureBoxNextFigure.TabIndex = 19;
            this.pictureBoxNextFigure.TabStop = false;
            // 
            // labelDifficulty
            // 
            this.labelDifficulty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelDifficulty.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F);
            this.labelDifficulty.ForeColor = System.Drawing.Color.Silver;
            this.labelDifficulty.Location = new System.Drawing.Point(6, 352);
            this.labelDifficulty.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.labelDifficulty.Name = "labelDifficulty";
            this.labelDifficulty.Size = new System.Drawing.Size(130, 53);
            this.labelDifficulty.TabIndex = 20;
            this.labelDifficulty.Text = "level10";
            this.labelDifficulty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelScoreBoard
            // 
            this.labelScoreBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScoreBoard.AutoSize = true;
            this.labelScoreBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelScoreBoard.ForeColor = System.Drawing.Color.Silver;
            this.labelScoreBoard.Location = new System.Drawing.Point(27, 426);
            this.labelScoreBoard.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.labelScoreBoard.Name = "labelScoreBoard";
            this.labelScoreBoard.Size = new System.Drawing.Size(110, 24);
            this.labelScoreBoard.TabIndex = 21;
            this.labelScoreBoard.Text = "ScoreBoard";
            this.labelScoreBoard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(338, 594);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "label3";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(310, 416);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(132, 21);
            this.comboBox1.TabIndex = 20;
            // 
            // labelExit
            // 
            this.labelExit.AutoSize = true;
            this.labelExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.labelExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.labelExit.Location = new System.Drawing.Point(572, 2);
            this.labelExit.Name = "labelExit";
            this.labelExit.Size = new System.Drawing.Size(23, 22);
            this.labelExit.TabIndex = 0;
            this.labelExit.Text = "X";
            this.labelExit.Click += new System.EventHandler(this.labelExit_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelHowToPlay
            // 
            this.labelHowToPlay.AutoSize = true;
            this.labelHowToPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.labelHowToPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHowToPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.labelHowToPlay.Location = new System.Drawing.Point(545, 2);
            this.labelHowToPlay.Name = "labelHowToPlay";
            this.labelHowToPlay.Size = new System.Drawing.Size(21, 22);
            this.labelHowToPlay.TabIndex = 1;
            this.labelHowToPlay.Text = "?";
            this.labelHowToPlay.Click += new System.EventHandler(this.labelHowToPlay_Click);
            // 
            // labelMusic
            // 
            this.labelMusic.AutoSize = true;
            this.labelMusic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.labelMusic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.labelMusic.Location = new System.Drawing.Point(515, 2);
            this.labelMusic.Name = "labelMusic";
            this.labelMusic.Size = new System.Drawing.Size(24, 22);
            this.labelMusic.TabIndex = 2;
            this.labelMusic.Text = "♫";
            this.labelMusic.Click += new System.EventHandler(this.labelMusic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(0)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(600, 800);
            this.Controls.Add(this.labelMusic);
            this.Controls.Add(this.labelHowToPlay);
            this.Controls.Add(this.labelExit);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNextFigure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelExit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxNextFigure;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label pauseLabel;
        private System.Windows.Forms.Label labelDifficulty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelGameEnded;
        private System.Windows.Forms.Label labelScoreBoard;
        private System.Windows.Forms.Label labelHowToPlay;
        private System.Windows.Forms.Label labelMusic;
    }
}

