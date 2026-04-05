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
            Control.CheckForIllegalCrossThreadCalls = true;
            ChessBoard  = new ChessBoardManega1(ChessBoard1, txbPlayerName1, pct1, lblPlayer1, lblPlayer2);
           
            socket = new SocketManeger();
            ChessBoard.PlayerMar += ChessBroad_PlayerMar;
            ChessBoard.EndGame += ChessBroad_EndGame;
            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            tmlCoolDown.Interval = Cons.COOL_DOWN_INVERVAL;
            ChessBoard.DrawChessBoard();
            
        }
        void EndGame()
        {
           ChessBoard1.Enabled = false;
            MessageBox.Show("End");
        }
        void ChessBroad_PlayerMar(object sender, ButtonClickEvent e)
        {
            tmlCoolDown.Start();
          
            prcbCoolDown.Value = 0;
            socket.Send(new SocketData((int)SocketCommad.SEND_POINT, "", e.ClickPoint));
            Listen();
        }
         void ChessBroad_EndGame(object sender, ButtonClickEvent e)
        {
           EndGame();
        }

       

        private void Form2_Load(object sender, EventArgs e)
        {
            txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txtIP.Text))
            {
                txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
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
            this.Hide();
            Trangchu o = new Trangchu();
            o.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Bang3 b = new Bang3();
            b.Show();
        }

        private void btLAN_Click(object sender, EventArgs e)
        {
            socket.IP = txtIP.Text;

            if(!socket.ConnectServer())
            {
               
                socket.CreateServer();
              
            }
            else
            {
                
                Listen();

               
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
             socket.Send(new SocketData((int)SocketCommad.SEND_POINT,"",e.ClickPoint));
        }
        private void ProcesData(SocketData data)
        {
            switch(data.Commad)
            {
                case (int)SocketCommad.NOTIFY:
                    MessageBox.Show(data.Message);
                        break;
                    case(int)SocketCommad.NEW_GAME:
                    break;
                    case(int)SocketCommad.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        ChessBoard.OtherPlayer(data.Point);
                        prcbCoolDown.Value = 0;
                        ChessBoard1.Enabled = true;
                        tmlCoolDown.Start();
                    }));


                    break;
                    case(int)SocketCommad.UNDO: break;
                    case(int)SocketCommad.QUIT: break;
                    default:
                    break;

            }
            Listen();
        }
        #endregion

        private void prcbCoolDown_Click(object sender, EventArgs e)
        {

        }

        private void tmlCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if(prcbCoolDown.Value>=prcbCoolDown.Maximum)
            {
                tmlCoolDown.Stop();
                EndGame ();
              
            }
        }
    }
   
    
}
