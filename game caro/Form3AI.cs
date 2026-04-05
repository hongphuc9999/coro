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
        ChessBoardManega ChessBoard;

        #endregion
        public Form3AI()
        {
            InitializeComponent();
            ChessBoard = new ChessBoardManega(pnlChessBoard, txbPlayerName, pct, lblPlayer1, lblPlayer2);
            ChessBoard.DrawChessBoard();
            newgame();
        }
        void newgame()
        {
            ChessBoard.ResetScore();
            ChessBoard.DrawChessBoard();
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
