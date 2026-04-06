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
    public partial class Form3AI : Form
    {
        #region Properties
        AI_5vs5 ChessBoardr;

        #endregion
        public Form3AI()
        {
            InitializeComponent();
            ChessBoardr = new AI_5vs5(pnlChessBoard, txbPlayerName2, pct2, lblPlayer1, lblPlayer2);
            ChessBoardr.DrawChessBoard();
            newgame();
        }
        void newgame()
        {
            ChessBoardr.ResetScore();
            ChessBoardr.DrawChessBoard();
        }

        private void Form3AI_Load(object sender, EventArgs e)
        {

        }

        private void btn_Click_Click(object sender, EventArgs e)
        {
            newgame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Trangchu v = new Trangchu();
            v.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form_3vs3 b = new Form_3vs3();
            b.Show();
            this.Hide();
        }
    }
}
