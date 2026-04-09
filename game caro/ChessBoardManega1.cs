using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    internal class ChessBoardManega1
    {
        #region Properties
        private Panel chessBoard;
        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
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
       
        private List<List<Button>> matrix;

        public List<List<Button>> Matrix
        {
            get {  return matrix; }
            set { matrix = value; }
        }

        public bool Enabled { get; internal set; }

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

        private Label lblPlayer1;
        private Label lblPlayer2;

        #endregion

        #region Initialize
        public ChessBoardManega1(Panel ChessBoard1, TextBox playerName1, PictureBox mark, Label lblPlayer1, Label lblPlayer2)
        {
            this.ChessBoard = ChessBoard1;
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
        public void DrawChessBoard()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();
            Matrix = new List<List<Button>>();
            Button ol = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(ol.Location.X + ol.Width, ol.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += btn_Click;
                    ChessBoard.Controls.Add(btn);
                    Matrix[i].Add(btn);
                    ol = btn;

                }
                ol.Location = new Point(0, ol.Location.Y + Cons.CHESS_HEIGHT);
                ol.Width = 0;
                ol.Height = 0;
            }
        }
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackgroundImage != null)
                return;

            Mark(btn);
            if (playerMar != null)
            {
                playerMar(this, new ButtonClickEvent(Toado(btn)));
            }
            // kiểm tra thắng trước 
            if (isEndgame(btn))
            {
                Endgame();
                return;
            }
           
            Changer();
            




        }
        public void OtherPlayer(Point point)
        {


            Button btn = Matrix[point.Y][point.X];

            if (btn.BackgroundImage != null)
                return;
           
            Mark(btn);

            // kiểm tra thắng trước 
            if (isEndgame(btn))
            {
                Endgame();
                return;
            }

            Changer();
        }
        public void Endgame()
        {
           

            Player[currentPlayer].Score++;

            MessageBox.Show(Player[currentPlayer].Name + " thắng!");

            // Gọi cập nhật UI
            UpdateScore();

            // Reset bàn cờ (nếu muốn)
            DrawChessBoard();

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
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }
            return countLeft + countRight >= 5;
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
            return countTop + countBottom >= 5;

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
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.X + i >= Cons.CHESS_BOARD_HEIGHT || point.Y + i >= Cons.CHESS_BOARD_WIDTH)
                    break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom >= 5;
        }
        private bool isEndsub(Button btn)
        {
            Point point = Toado(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i > Cons.CHESS_BOARD_WIDTH || point.Y - i < 0)
                    break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }

            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i < 0)
                    break;
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }

            return countTop + countBottom >= 5;
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
    public class ButtonClickEvent : EventArgs
    {
        private Point clickPoint;

        public Point ClickPoint
        {
            get { return clickPoint; }
            set { clickPoint = value; }
        }

        public ButtonClickEvent(Point point)
        {
            this.clickPoint = point;
        }
    }
}

