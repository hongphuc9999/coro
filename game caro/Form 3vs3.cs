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
    
    public partial class Form_3vs3 : Form
    {
        #region Properties
        AI_3vs3 Bang;
        #endregion
        public Form_3vs3()
        {
            InitializeComponent();
            Bang = new AI_3vs3(pnlBangba, txbPlayerName, pct, lblPlayer1, lblPlayer2);
            Bang.DrawChessBoard1();
            newgame();
        }
        void newgame()
        {
            Bang.ResetScore();
            Bang.DrawChessBoard1();
        }
        private void Form_3vs3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           Form3AI v = new Form3AI();
            v.Show();
            this.Hide();
        }

        private void btnXOA_Click(object sender, EventArgs e)
        {
            newgame();
        }

        private void btnTHOAT_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trangchu v = new Trangchu();
            v.Show();
        }
    }
}
