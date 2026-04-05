using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    internal class Bangba
    {
        #region Properties
        private Panel bang;
        public Panel Bang
        {
            get { return bang; }
            set { bang = value; }
        }
        private List<Player> player;
        public List<Player> Player
        {
            get { return player; }
            set { player = value; }
        }


        private int currentPlayer;


        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        private TextBox playerName;
        public TextBox PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
        private PictureBox playerMark;
        public PictureBox PlayerMark
        {
            get { return playerMark; }
            set { playerMark = value; }
        }
        private List<List<Button>> Matrix;
        public List<List<Button>> matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        public bool Enabled
        {
            get { return bang.Enabled; }
            set { bang.Enabled = value; }
        }

        private event EventHandler<ButtonClickEvent> playerMar;
        public event EventHandler<ButtonClickEvent> PlayerMar
        {
            add
            {
                playerMar += value;
            }
            remove
            {
                playerMar -= value;
            }
        }
        private event EventHandler<ButtonClickEvent> endGame;
        public event EventHandler<ButtonClickEvent> EndGame
        {
            add
            {
                endGame += value;
            }
            remove
            {
                endGame -= value;
            }
        }
        private event EventHandler drawGame;
        public event EventHandler DrawGame
        {
            add { drawGame += value; }
            remove { drawGame -= value; }
        }
        private Label lblPlayer1;
        private Label lblPlayer2;
        private int countStep = 0;
        public int LastWinner { get; private set; } = -1;
        #endregion

        #region Initialize
        public Bangba(Panel Bang, TextBox playerName1, PictureBox mark, Label lblPlayer1, Label lblPlayer2)
        {
            this.bang = Bang;
            this.playerName = playerName1;
            this.playerMark = mark;
            this.lblPlayer1 = lblPlayer1;
            this.lblPlayer2 = lblPlayer2;

            this.player = new List<Player>()
            {
                new Player ("Player 1",Image.FromFile(Application.StartupPath + "\\Resources\\anh3.png")),
                new Player("Player 2",Image.FromFile(Application.StartupPath + "\\Resources\\anh4.png"))
            };
            currentPlayer = 0;
            Changer();
            
        }



        #endregion
        #region Methods
        public void DrawChessBoard1()
        {
            Bang.Controls.Clear();
            Matrix = new List<List<Button>>();
            Button ol = new Button() { Width = 0, Location = new Point(0, 0) };
            countStep = 0;
            for (int i = 0; i < taobang.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < taobang.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = taobang.CHESS_WIDTH,
                        Height = taobang.CHESS_HEIGHT,
                        Location = new Point(ol.Location.X + ol.Width, ol.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += btn_Click;
                    Bang.Controls.Add(btn);
                    Matrix[i].Add(btn);
                    ol = btn;

                }
                ol.Location = new Point(0, ol.Location.Y + taobang.CHESS_HEIGHT);
                ol.Width = 0;
                ol.Height = 0;
               
            }
        }
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackgroundImage != null)
                return;
            int playerBeforeMove = currentPlayer;
            Mark(btn);
           
            countStep++;

            
            if (isEndgame(btn))
            {
                Endgame(playerBeforeMove);
                return;
            }
            int totalCells = taobang.CHESS_BOARD_HEIGHT * taobang.CHESS_BOARD_WIDTH;
            if (countStep >= totalCells)
            {
                MessageBox.Show("Ván cờ hòa!");

                if (drawGame != null)
                    drawGame(this, EventArgs.Empty);
                DrawChessBoard1();
                bang.Enabled = true;
                return;
             

               
            }
            if (playerMar != null)
            {
                playerMar(this, new ButtonClickEvent(Toado(btn)));
            }
            Changer();


        }
        public void OtherPlayer(Point point)
        {


            Button btn = Matrix[point.Y][point.X];

            if (btn.BackgroundImage != null)
                return;
            int playerBeforeMove = currentPlayer;
            Mark(btn);
            countStep++;
            // kiểm tra thắng trước 
            if (isEndgame(btn))
            {
                Endgame(playerBeforeMove);
                return;
            }
            int totalCells = taobang.CHESS_BOARD_HEIGHT * taobang.CHESS_BOARD_WIDTH;
            if (countStep >= totalCells)
            {
                
                MessageBox.Show("Ván cờ hòa!");
              
                DrawChessBoard1();
                bang.Enabled = true;
                return;
            }
            Changer();
        }
        public void RemoteDraw()
        {
            MessageBox.Show("Ván cờ hòa!");
            DrawChessBoard1();
            bang.Enabled = true;
        }
        public void RemoteWin(int winnerIndex)
        {
            Player[winnerIndex].Score++;
            MessageBox.Show(Player[winnerIndex].Name + " thắng!");
            UpdateScore();
            DrawChessBoard1();
            bang.Enabled = true;
        }
        private void Endgame(int winnerIndex)
        {
          //  Player[currentPlayer].Score++;
            LastWinner = winnerIndex;
           // MessageBox.Show(Player[currentPlayer].Name + " thắng!");

            Player[winnerIndex].Score++;
            MessageBox.Show(Player[winnerIndex].Name + " thắng!");
            UpdateScore();
            if (endGame != null)
                endGame(this, new ButtonClickEvent(new Point(winnerIndex, -1)));

            DrawChessBoard1();
            bang.Enabled = true;
        }
        private void EndgameRemote(int winnerIndex)
        {
            Player[winnerIndex].Score++;
            MessageBox.Show(Player[winnerIndex].Name + " thắng!\nVán mới bắt đầu...");
            UpdateScore();
            DrawChessBoard1();
            bang.Enabled = true;
        }
        private void UpdateScore()
        {
            lblPlayer1.Text = player[0].Name + " " + player[0].Score;
            lblPlayer2.Text = player[1].Name + " " + player[1].Score;
        }
        public void ResetScore()
        {
            player[0].Score = 0;
            player[1].Score = 0;

            UpdateScore();
        }

        private bool isEndgame(Button btn)
        {
            return isEndHorizontal(btn)
                || isEndVertical(btn)
                || isEndPrimary(btn)
                || isEndsub(btn);
        }


        private Point Toado(Button btn)
        {

            int hangngang = Convert.ToInt32(btn.Tag);
            int hangdoc = Matrix[hangngang].IndexOf(btn);
            Point point = new Point(hangdoc, hangngang);

            return point;
        }
        private bool isEndHorizontal(Button btn)
        {
            Point point = Toado(btn);
            int countLeft = 0;
            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                    break;
            }

            int countRight = 0;
            for (int i = point.X + 1; i < taobang.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }
            return countLeft + countRight >= 3;
        }
        private bool isEndVertical(Button btn)
        {

            Point point = Toado(btn);
            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = point.Y + 1; i < Matrix.Count; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }
            return countTop + countBottom >= 3;

        }
        private bool isEndPrimary(Button btn)
        {
            Point point = Toado(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= taobang.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.X + i >= taobang.CHESS_BOARD_WIDTH || point.Y + i >= taobang.CHESS_BOARD_HEIGHT)
                    break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom >= 3;
        }
        private bool isEndsub(Button btn)
        {
            Point point = Toado(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > taobang.CHESS_BOARD_WIDTH || point.Y - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= taobang.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= taobang.CHESS_BOARD_HEIGHT || point.X - i < 0)
                    break;
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom >= 3;
        }

        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }
        private void Changer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;
            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }
       
    }
    #endregion
}

