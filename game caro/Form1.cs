using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManega ChessBoard;
      
        #endregion
        public Form1()
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

        private void UpdateScore()
        {
            throw new NotImplementedException();
        }

        private void btn_Click_Click(object sender, EventArgs e)
        {
           newgame();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 v = new Form2();
            v.Show();
        }
    }
}
