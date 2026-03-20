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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            ChessBoard  = new ChessBoardManega1(ChessBoard1, txbPlayerName1, pct1, lblPlayer1, lblPlayer2);
            ChessBoard.DrawChessBoard();
        }

        internal ChessBoardManega1 ChessBoard { get; }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Newgame();
        }
        void Newgame()
        {
            ChessBoard.DrawChessBoard();
            ChessBoard.ResetScore();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
