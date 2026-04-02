using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace game_caro
{
    public partial class _3_vs_3_AI : Form
    {
        #region Properties
        _3vs3_AI Bang;
        #endregion
        public _3_vs_3_AI()
        {
            InitializeComponent();
            Bang = new _3vs3_AI (pnlBangba, txbPlayerName, pct, lblPlayer1, lblPlayer2);
            Bang.DrawChessBoard1();
            newgame();
        }

        

        void newgame()
        {
            Bang.ResetScore();
            Bang.DrawChessBoard1();
        }

        private void _3_vs_3_AI_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 a = new Form1();
            a.Show();
        }

       
    }
}
