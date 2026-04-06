namespace game_caro
{
    partial class Form3AI
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPlayer2 = new System.Windows.Forms.Label();
            this.lblPlayer1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtAIkho = new System.Windows.Forms.Button();
            this.pct2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Click = new System.Windows.Forms.Button();
            this.txbPlayerName2 = new System.Windows.Forms.TextBox();
            this.pnlChessBoard = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pct2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel2.Controls.Add(this.lblPlayer2);
            this.panel2.Controls.Add(this.lblPlayer1);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.txtAIkho);
            this.panel2.Controls.Add(this.pct2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btn_Click);
            this.panel2.Controls.Add(this.txbPlayerName2);
            this.panel2.Location = new System.Drawing.Point(818, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(317, 639);
            this.panel2.TabIndex = 3;
            // 
            // lblPlayer2
            // 
            this.lblPlayer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer2.Location = new System.Drawing.Point(184, 169);
            this.lblPlayer2.Name = "lblPlayer2";
            this.lblPlayer2.Size = new System.Drawing.Size(110, 74);
            this.lblPlayer2.TabIndex = 9;
            // 
            // lblPlayer1
            // 
            this.lblPlayer1.BackColor = System.Drawing.Color.Bisque;
            this.lblPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayer1.Location = new System.Drawing.Point(13, 169);
            this.lblPlayer1.Name = "lblPlayer1";
            this.lblPlayer1.Size = new System.Drawing.Size(111, 74);
            this.lblPlayer1.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(90, 568);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 39);
            this.button2.TabIndex = 7;
            this.button2.Text = "Thoát ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtAIkho
            // 
            this.txtAIkho.BackColor = System.Drawing.Color.MistyRose;
            this.txtAIkho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAIkho.Location = new System.Drawing.Point(102, 452);
            this.txtAIkho.Name = "txtAIkho";
            this.txtAIkho.Size = new System.Drawing.Size(115, 46);
            this.txtAIkho.TabIndex = 6;
            this.txtAIkho.Text = "3 x 3 ";
            this.txtAIkho.UseVisualStyleBackColor = false;
            this.txtAIkho.Click += new System.EventHandler(this.button1_Click);
            // 
            // pct2
            // 
            this.pct2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.pct2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pct2.Location = new System.Drawing.Point(3, 3);
            this.pct2.Name = "pct2";
            this.pct2.Size = new System.Drawing.Size(121, 120);
            this.pct2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pct2.TabIndex = 5;
            this.pct2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::game_caro.Properties.Resources.anh5;
            this.pictureBox1.Location = new System.Drawing.Point(140, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 140);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Click
            // 
            this.btn_Click.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Click.Location = new System.Drawing.Point(90, 504);
            this.btn_Click.Name = "btn_Click";
            this.btn_Click.Size = new System.Drawing.Size(136, 39);
            this.btn_Click.TabIndex = 1;
            this.btn_Click.Text = "Xóa ";
            this.btn_Click.UseVisualStyleBackColor = true;
            this.btn_Click.Click += new System.EventHandler(this.btn_Click_Click);
            // 
            // txbPlayerName2
            // 
            this.txbPlayerName2.Location = new System.Drawing.Point(3, 129);
            this.txbPlayerName2.Name = "txbPlayerName2";
            this.txbPlayerName2.Size = new System.Drawing.Size(121, 22);
            this.txbPlayerName2.TabIndex = 0;
            // 
            // pnlChessBoard
            // 
            this.pnlChessBoard.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlChessBoard.Location = new System.Drawing.Point(11, 13);
            this.pnlChessBoard.Name = "pnlChessBoard";
            this.pnlChessBoard.Size = new System.Drawing.Size(801, 636);
            this.pnlChessBoard.TabIndex = 2;
            // 
            // Form3AI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 659);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlChessBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3AI";
            this.Text = "Form3AI";
            this.Load += new System.EventHandler(this.Form3AI_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pct2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblPlayer2;
        private System.Windows.Forms.Label lblPlayer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button txtAIkho;
        private System.Windows.Forms.PictureBox pct2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Click;
        private System.Windows.Forms.TextBox txbPlayerName2;
        private System.Windows.Forms.Panel pnlChessBoard;
    }
}