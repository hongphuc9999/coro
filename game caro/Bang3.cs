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
    public partial class Bang3 : Form
    {
        #region Properties
        Bangba Bang;
        #endregion
        public Bang3()
        {
            InitializeComponent();
            Bang = new Bangba(pnlBangba, txbPlayerName, pct, lblPlayer1, lblPlayer2);
            Bang.DrawChessBoard1();
            newgame();
        }
        void newgame()
        {
            Bang.ResetScore();
            Bang.DrawChessBoard1();
        }
        private void Bang3_Load(object sender, EventArgs e)
        {

        }

        private void btnXOA_Click(object sender, EventArgs e)
        {
            newgame();
        }

        private void btnTHOAT_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trangchu o = new Trangchu();
            o.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 b = new Form2();
            b.Show();
        }

        
    }
}
