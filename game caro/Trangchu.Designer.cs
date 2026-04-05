namespace game_caro
{
    partial class Trangchu
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
            this.bt3x3 = new System.Windows.Forms.Button();
            this.bt5vs5 = new System.Windows.Forms.Button();
            this.btnthoat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.chếĐộAIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trungBìnhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.khóToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt3x3
            // 
            this.bt3x3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt3x3.Location = new System.Drawing.Point(159, 26);
            this.bt3x3.Name = "bt3x3";
            this.bt3x3.Size = new System.Drawing.Size(215, 52);
            this.bt3x3.TabIndex = 0;
            this.bt3x3.Text = "3 vs 3";
            this.bt3x3.UseVisualStyleBackColor = true;
            this.bt3x3.Click += new System.EventHandler(this.bt3x3_Click);
            // 
            // bt5vs5
            // 
            this.bt5vs5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt5vs5.Location = new System.Drawing.Point(159, 99);
            this.bt5vs5.Name = "bt5vs5";
            this.bt5vs5.Size = new System.Drawing.Size(215, 54);
            this.bt5vs5.TabIndex = 1;
            this.bt5vs5.Text = "5 vs 5";
            this.bt5vs5.UseVisualStyleBackColor = true;
            this.bt5vs5.Click += new System.EventHandler(this.bt5vs5_Click);
            // 
            // btnthoat
            // 
            this.btnthoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnthoat.Location = new System.Drawing.Point(355, 453);
            this.btnthoat.Name = "btnthoat";
            this.btnthoat.Size = new System.Drawing.Size(146, 60);
            this.btnthoat.TabIndex = 3;
            this.btnthoat.Text = "Thoát";
            this.btnthoat.UseVisualStyleBackColor = true;
            this.btnthoat.Click += new System.EventHandler(this.btnthoat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(257, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 91);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cờ Caro";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.bt5vs5);
            this.groupBox1.Controls.Add(this.bt3x3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(162, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 269);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chế Độ Game";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(174, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 52);
            this.panel1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chếĐộAIToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(174, 52);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // chếĐộAIToolStripMenuItem
            // 
            this.chếĐộAIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trungBìnhToolStripMenuItem,
            this.khóToolStripMenuItem});
            this.chếĐộAIToolStripMenuItem.Name = "chếĐộAIToolStripMenuItem";
            this.chếĐộAIToolStripMenuItem.Size = new System.Drawing.Size(152, 48);
            this.chếĐộAIToolStripMenuItem.Text = "Chế Độ AI";
            // 
            // trungBìnhToolStripMenuItem
            // 
            this.trungBìnhToolStripMenuItem.Name = "trungBìnhToolStripMenuItem";
            this.trungBìnhToolStripMenuItem.Size = new System.Drawing.Size(243, 36);
            this.trungBìnhToolStripMenuItem.Text = "Trung Bình";
            this.trungBìnhToolStripMenuItem.Click += new System.EventHandler(this.trungBìnhToolStripMenuItem_Click);
            // 
            // khóToolStripMenuItem
            // 
            this.khóToolStripMenuItem.Name = "khóToolStripMenuItem";
            this.khóToolStripMenuItem.Size = new System.Drawing.Size(243, 36);
            this.khóToolStripMenuItem.Text = "Khó";
            this.khóToolStripMenuItem.Click += new System.EventHandler(this.khóToolStripMenuItem_Click);
            // 
            // Trangchu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.ClientSize = new System.Drawing.Size(854, 525);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnthoat);
            this.Controls.Add(this.label1);
            this.Name = "Trangchu";
            this.Text = "Trangchu";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnthoat;
        private System.Windows.Forms.Button bt5vs5;
        private System.Windows.Forms.Button bt3x3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem chếĐộAIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trungBìnhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem khóToolStripMenuItem;
    }
}