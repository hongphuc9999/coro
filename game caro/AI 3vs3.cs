using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    internal class AI_3vs3
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
        private Label lblPlayer1;
        private Label lblPlayer2;
        private int countStep = 0;
        private bool isPlayerTurn;
        #endregion
        #region Initialize
        public AI_3vs3(Panel Bang, TextBox playerName1, PictureBox mark, Label lblPlayer1, Label lblPlayer2)
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
                countStep = 0;
            }
        }
        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null) return;

            // 1. Lượt của NGƯỜI CHƠI đánh
            Mark(btn);
            countStep++;

            if (isEndgame(btn)) { Endgame(); return; }
            if (countStep == 9) { MessageBox.Show("Hòa!"); DrawChessBoard1(); return; }

            // 2. Đổi sang lượt AI
            Changer();

            // 3. Gọi AI đánh (AI sẽ đánh bất kể quân gì, miễn là đến lượt nó)
            // Thay vì check currentPlayer == 1, ta check xem có phải lượt AI không
            if (!isPlayerTurn) // Bạn nên có một biến bool isPlayerTurn để quản lý
            {
                AIMove();
            }
        }


        private void Endgame()
        {
            Player[currentPlayer].Score++;

            MessageBox.Show(Player[currentPlayer].Name + " thắng!");


            UpdateScore();


            DrawChessBoard1();

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
        private void AIMove()
        {
            // Xác định index của AI và Người chơi trong mảng Player[]
            int aiIndex = currentPlayer;
            int humanIndex = (currentPlayer == 0) ? 1 : 0;

            // 1. AI tìm nước để THẮNG (Tấn công)
            if (TryToWinOrBlock(aiIndex)) return;

            // 2. AI tìm nước để CHẶN Người chơi (Phòng ngự)
            if (TryToWinOrBlock(humanIndex)) return;

            // 3. Nếu không có nước nguy hiểm, đánh ô giữa hoặc ô trống đầu tiên
            if (Matrix[1][1].BackgroundImage == null)
            {
                Mark(Matrix[1][1]);
                ExecuteAfterMove(Matrix[1][1]);
                return;
            }

            // Chọn ô trống bất kỳ
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (Matrix[i][j].BackgroundImage == null)
                    {
                        Mark(Matrix[i][j]);
                        ExecuteAfterMove(Matrix[i][j]);
                        return;
                    }
        }

        // Hàm bổ trợ để tìm nước thắng hoặc chặn
        private bool TryToWinOrBlock(int playerIndex)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Matrix[i][j].BackgroundImage == null)
                    {
                        // Giả lập đánh thử vào ô này
                        Matrix[i][j].BackgroundImage = Player[playerIndex].Mark;

                        if (isEndgame(Matrix[i][j])) // Nếu đánh vào đây mà thắng
                        {
                            Matrix[i][j].BackgroundImage = null; // Trả lại trạng thái cũ
                            Mark(Matrix[i][j]); // Đánh thật
                            ExecuteAfterMove(Matrix[i][j]);
                            return true;
                        }
                        Matrix[i][j].BackgroundImage = null; // Trả lại trạng thái cũ
                    }
                }
            }
            return false;
        }

        // Hàm xử lý sau khi AI đánh xong (kiểm tra thắng/hòa/đổi lượt)
        private void ExecuteAfterMove(Button btn)
        {
            countStep++;
            if (isEndgame(btn))
            {
                Endgame();
            }
            else if (countStep == 9)
            {
                MessageBox.Show("Hòa!");
                DrawChessBoard1();
            }
            else
            {
                Changer(); // Đổi lại lượt cho người chơi
            }
        }
    }
    #endregion
}

