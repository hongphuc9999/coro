using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static game_caro.SocketData;

namespace game_caro
{
    public partial class Bang3 : Form
    {
        #region Properties
        Bangba Bang;
        SocketManeger socket;
        #endregion
        public Bang3()
        {
            InitializeComponent();
            socket = new SocketManeger();

            // Đăng ký các Event từ Socket
            socket.Connected += Socket_Connected;
            socket.DataReceived += Socket_DataReceived;

            Bang = new Bangba(pnlBangba, txbPlayerName, pct, lblPlayer1, lblPlayer2);
            Bang.PlayerMar += ChessBroad_PlayerMar;
            Bang.EndGame += ChessBroad_EndGame;
            Bang.DrawGame += ChessBroad_DrawGame;

            prcCoolDown1.Step = Cons.COOL_DOWN_STEP;
            prcCoolDown1.Maximum = Cons.COOL_DOWN_TIME;
            prcCoolDown1.Value = 0;
            tmlCoolDown.Interval = Cons.COOL_DOWN_INVERVAL;

            Bang.DrawChessBoard1();
            newgame();

            // Khóa bàn cờ lúc mới vào, chờ kết nối LAN mới mở
            Bang.Enabled = false;
        }

        private void Socket_Connected()
        {
            this.Invoke((MethodInvoker)(() =>
            {
                MessageBox.Show("Đã kết nối 2 máy thành công! Bắt đầu chơi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Host được đi trước, Client phải đợi dữ liệu tới
                if (socket.IsServer)
                {
                    Bang.Enabled = true;
                    pnlBangba.Enabled = true;
                }
                else
                {
                    Bang.Enabled = false; // Chờ Host đánh trước
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
                    case (int)SocketData.SocketCommad.NOTIFY:
                        MessageBox.Show(data.Message);
                        break;

                    case (int)SocketData.SocketCommad.SEND_POINT:
                        Bang.OtherPlayer(data.Point);
                        prcCoolDown1.Value = 0;
                        Bang.Enabled = true; // Đối thủ đánh xong, mở bàn cờ cho mình
                        pnlBangba.Enabled = true;
                        tmlCoolDown.Start();
                        break;

                    case (int)SocketData.SocketCommad.WIN:
                        int winnerIndex = data.Point.X;
                        Bang.RemoteWin(winnerIndex);
                        prcCoolDown1.Value = 0;
                        tmlCoolDown.Stop();
                        Bang.Enabled = false; // Hết game thì khóa lại
                        break;

                    case (int)SocketData.SocketCommad.DRAW:
                        Bang.RemoteDraw();
                        prcCoolDown1.Value = 0;
                        tmlCoolDown.Stop();
                        Bang.Enabled = false;
                        break;
                }
            }));
        }

        void ChessBroad_PlayerMar(object sender, ButtonClickEvent e)
        {
            tmlCoolDown.Start();
            prcCoolDown1.Value = 0;

            // Gửi tọa độ cho máy kia
            socket.Send(new SocketData((int)SocketData.SocketCommad.SEND_POINT, "", e.ClickPoint));

            // Đánh xong thì khóa bàn cờ của mình lại, chờ máy kia đánh
            Bang.Enabled = false;
            pnlBangba.Enabled = false;
        }

        void ChessBroad_EndGame(object sender, ButtonClickEvent e)
        {
            int winnerIndex = e.ClickPoint.X;
            socket.Send(new SocketData((int)SocketData.SocketCommad.WIN, "", new Point(winnerIndex, -1)));
            Bang.Enabled = false;
            tmlCoolDown.Stop();
            prcCoolDown1.Value = 0;
            EndGame();
        }

        void ChessBroad_DrawGame(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketData.SocketCommad.DRAW, "", new Point(0, 0)));
            Bang.Enabled = false;
            tmlCoolDown.Stop();
            prcCoolDown1.Value = 0;
        }

        void newgame()
        {
            Bang.ResetScore();
            Bang.DrawChessBoard1();
        }

        private void Bang3_Load(object sender, EventArgs e)
        {
            txtIP.Text = "";
        }

        private void btnXOA_Click(object sender, EventArgs e)
        {
            newgame();
        }

        private void btnTHOAT_Click(object sender, EventArgs e)
        {
            socket.CloseConnection();
            this.Hide();
            Trangchu o = new Trangchu();
            o.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            socket.CloseConnection();
            this.Hide();
            Form2 b = new Form2();
            b.Show();
        }

        private void txtIP_Click(object sender, EventArgs e)
        {
            string inputIP = txtIP.Text.Trim();

            if (string.IsNullOrEmpty(inputIP))
            {
                // Máy 1: Làm Host
                socket.CreateServer();
                txtIP.Enabled = false;
                btnLAN.Enabled = false;
            }
            else
            {
                // Máy 2: Làm Client
                if (socket.ConnectServer(inputIP))
                {
                    txtIP.Enabled = false;
                    btnLAN.Enabled = false;
                }
            }
        }

        void EndGame()
        {
            Bang.Enabled = false;
            MessageBox.Show("Trò chơi kết thúc!");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            prcCoolDown1.PerformStep();

            if (prcCoolDown1.Value >= prcCoolDown1.Maximum)
            {
                tmlCoolDown.Stop();
                EndGame();
            }
        }

        private void prcCoolDown1_Click(object sender, EventArgs e) { }
    }

}
