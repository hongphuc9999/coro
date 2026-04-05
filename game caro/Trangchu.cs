using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    public partial class Trangchu : Form
    {
        public Trangchu()
        {
            InitializeComponent();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt5vs5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 b = new Form2();
            b.Show();
        }

        private void bt3x3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bang3 b = new Bang3();
            b.Show();
        }

        private void trungBìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            Form1 a = new Form1();
            a.Show();

        }

        private void khóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3AI v = new Form3AI();
            v.Show();
        }
    }
}
