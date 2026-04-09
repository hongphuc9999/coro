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
            socket = new SocketManeger();
            InitializeComponent();
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
        }
        void ChessBroad_EndGame(object sender, ButtonClickEvent e)
        {
            int winnerIndex = e.ClickPoint.X;
            socket.Send(new SocketData((int)SocketCommad.WIN, "", new Point(winnerIndex, -1)));
            Bang.Enabled = false;
            tmlCoolDown.Stop();
            prcCoolDown1.Value = 0;
            EndGame();
            Listen();
        }
        void ChessBroad_DrawGame(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketCommad.DRAW, "", new Point(0, 0)));
            Bang.Enabled = false;
            tmlCoolDown.Stop();
            prcCoolDown1.Value = 0;
            Listen();
        }
        void newgame()
        {
            Bang.ResetScore();
            Bang.DrawChessBoard1();
        }
        void ChessBroad_PlayerMar(object sender, ButtonClickEvent e)
        {
            tmlCoolDown.Start();
            prcCoolDown1.Value = 0;
            socket.Send(new SocketData((int)SocketCommad.SEND_POINT, "", e.ClickPoint));
            Bang.Enabled = false;
            Listen();
        }
        private void Bang3_Load(object sender, EventArgs e)
        {
            txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txtIP.Text))
            {
                txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
        void Listen()
        {


            Thread listenthread = new Thread(() =>
            {

                try
                {
                    SocketData data = (SocketData)socket.Receive();
                    ProcesData(data);
                }
                catch
                {

                }

            });
            listenthread.IsBackground = true;
            listenthread.Start();




        }
        void ChessBroad(object sender, ButtonClickEvent e)
        {
            socket.Send(new SocketData((int)SocketCommad.SEND_POINT, "", e.ClickPoint));
        }
        private void ProcesData(SocketData data)
        {
            switch (data.Commad)
            {
                case (int)SocketCommad.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommad.NEW_GAME:
                    break;
                case (int)SocketCommad.SEND_POINT:
                   
                    this.Invoke((MethodInvoker)(() =>
                    {
                        Bang.OtherPlayer(data.Point);
                        prcCoolDown1.Value = 0;
                        Bang.Enabled = true;
                        tmlCoolDown.Start();
                    }));
                    break;
                case (int)SocketCommad.WIN:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        int winnerIndex = data.Point.X;
                        Bang.RemoteWin(winnerIndex);
                        prcCoolDown1.Value = 0;
                        Bang.Enabled = true;
                        tmlCoolDown.Stop();
                    }));
                    break;
                case (int)SocketCommad.DRAW:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        Bang.RemoteDraw();
                        prcCoolDown1.Value = 0;
                        Bang.Enabled = true;
                        tmlCoolDown.Stop();
                    }));
                    break;
                case (int)SocketCommad.UNDO: break;
                case (int)SocketCommad.QUIT: break;
                default:
                    break;

            }
            if (data.Commad != (int)SocketCommad.WIN && data.Commad != (int)SocketCommad.DRAW)
            {
                Listen();
            }
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

        private void txtIP_Click(object sender, EventArgs e)
        {
           string inputIP = txtIP.Text.Trim();

            if (string.IsNullOrEmpty(inputIP))
            {
                socket.CreateServer();
                Listen();
            }
            else
            {
                socket.IP = inputIP;
                if (socket.ConnectServer())
                {
                    Listen();
                }
                


            }
        }
        void EndGame()
        {
           Bang.Enabled = false;
            MessageBox.Show("End");
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

        private void prcCoolDown1_Click(object sender, EventArgs e)
        {

        }
    }
}
