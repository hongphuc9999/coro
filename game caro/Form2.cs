using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static game_caro.SocketData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace game_caro
{
    public partial class Form2 : Form
    {
        #region Properties
        ChessBoardManega1 ChessBoard;
        SocketManeger socket;
      
     

        public Form2()
        {
            InitializeComponent();

            ChessBoard = new ChessBoardManega1(ChessBoard1, txbPlayerName1, pct1, lblPlayer1, lblPlayer2);
            socket = new SocketManeger();

            // Đăng ký sự kiện lắng nghe từ SocketManeger
            socket.Connected += Socket_Connected;
            socket.DataReceived += Socket_DataReceived;

            ChessBoard.PlayerMar += ChessBroad_PlayerMar;
            ChessBoard.EndGame += ChessBroad_EndGame;

            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            tmlCoolDown.Interval = Cons.COOL_DOWN_INVERVAL;

            ChessBoard.DrawChessBoard();

            // Lúc mới mở Form, khóa bàn cờ lại chờ kết nối mạng
            ChessBoard1.Enabled = false;
        }

        #region Socket Events (Xử lý mạng LAN)
        private void Socket_Connected()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                MessageBox.Show("Đã kết nối 2 máy thành công! Bắt đầu chơi Caro.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Máy làm Host (tạo phòng) sẽ được đi trước
                if (socket.IsServer)
                {
                    ChessBoard1.Enabled = true;
                }
                else
                {
                    ChessBoard1.Enabled = false; // Máy Client chờ Host đi trước
                }
            }));
        }

        private void Socket_DataReceived(SocketData data)
        {
            ProcesData(data);
        }

        private void ProcesData(SocketData data)
        {
            if (data == null) return;

            this.Invoke((MethodInvoker)(() =>
            {
                switch (data.Commad)
                {
                    case (int)SocketCommad.NOTIFY:
                        MessageBox.Show(data.Message);
                        break;

                    case (int)SocketCommad.NEW_GAME:
                        // Xử lý tạo game mới nếu cần
                        break;

                    case (int)SocketCommad.SEND_POINT:
                        // Đối thủ đã đánh, vẽ lên bàn cờ của mình
                        ChessBoard.OtherPlayer(data.Point);
                        prcbCoolDown.Value = 0;
                        tmlCoolDown.Start();

                        // Mở khóa bàn cờ để đến lượt mình đánh
                        ChessBoard1.Enabled = true;
                        break;

                    case (int)SocketCommad.WIN:
                        tmlCoolDown.Stop();
                        ChessBoard1.Enabled = false;
                        MessageBox.Show("Đối thủ đã chiến thắng!", "Kết thúc");
                        break;

                    case (int)SocketCommad.UNDO:
                        break;

                    case (int)SocketCommad.QUIT:
                        tmlCoolDown.Stop();
                        ChessBoard1.Enabled = false;
                        MessageBox.Show("Đối thủ đã thoát khỏi trò chơi.", "Thông báo");
                        break;
                }
            }));
        }
        #endregion

        #region Game Logic & UI Events
        void ChessBroad_PlayerMar(object sender, ButtonClickEvent e)
        {
            tmlCoolDown.Start();
            prcbCoolDown.Value = 0;

            // Gửi tọa độ nước đi sang máy đối thủ
            socket.Send(new SocketData((int)SocketCommad.SEND_POINT, "", e.ClickPoint));

            // Đánh xong thì khóa bàn cờ của mình lại, chờ đối thủ đánh
            ChessBoard1.Enabled = false;
        }

        void ChessBroad_EndGame(object sender, ButtonClickEvent e)
        {
            // Báo cho máy kia biết mình đã thắng
            socket.Send(new SocketData((int)SocketCommad.WIN, "", e.ClickPoint));
            EndGame();
        }

        void EndGame()
        {
            tmlCoolDown.Stop();
            prcbCoolDown.Value = 0;
            ChessBoard1.Enabled = false;
            MessageBox.Show("Trò chơi kết thúc!", "Thông báo");
        }

        private void tmlCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                tmlCoolDown.Stop();
                EndGame();
            }
        }
        #endregion

        #region Nút bấm giao diện
        // Giả sử nút "LAN" của bạn tên là btLAN
        private void btLAN_Click(object sender, EventArgs e)
        {
            string inputIP = txtIP.Text.Trim();

            if (string.IsNullOrEmpty(inputIP))
            {
                // Nếu để trống IP -> Máy này làm HOST
                socket.CreateServer();
                txtIP.Enabled = false;
                btLAN.Enabled = false;
            }
            else
            {
                // Nếu nhập IP -> Máy này làm CLIENT
                if (socket.ConnectServer(inputIP))
                {
                    txtIP.Enabled = false;
                    btLAN.Enabled = false;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Reset lại game (nếu bạn đã viết hàm reset trong ChessBoardManega1)
            ChessBoard.DrawChessBoard();
            prcbCoolDown.Value = 0;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketCommad.QUIT, "", new Point()));
            socket.CloseConnection();
            this.Hide();
            Trangchu o = new Trangchu();
            o.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            socket.CloseConnection();
            this.Hide();
            Bang3 b = new Bang3();
            b.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtIP.Text = "";
        }

        private void prcbCoolDown_Click(object sender, EventArgs e) { }
        #endregion
    }
    #endregion

}
