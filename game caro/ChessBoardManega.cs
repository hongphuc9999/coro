using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    public class ChessBoardManega
    {
        #region Properties
        private Panel chessBoard;
        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }
        private List<Player> player;
       public List<Player> Player { 
            get { return player; }
            set { player = value; }
        }


        private int currentPlayer;
        

        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set {  currentPlayer = value; }
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
        private List<List<Button>>Matrix ;
        private Label lblPlayer1;
        private Label lblPlayer2;
        int[] attackScore = { 0, 9, 54, 162, 1458, 13122 };
        int[] defenseScore = { 0, 3, 27, 81, 729, 6561 };
        #endregion

        #region Initialize
        public ChessBoardManega(Panel chessBoard, TextBox playerName, PictureBox mark, Label lblPlayer1, Label lblPlayer2)
        {
            this.ChessBoard = chessBoard;
            this.playerName = playerName;
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
            player[0].Name = "";
            player[1].Name = "";

            UpdateScore();

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

            //  KIỂM TRA THẮNG TRƯỚC
            if (isEndgame(btn))
            {
                Endgame();
                return;
            }

            Changer();

            // AI đi
            AITurn();
        }
        private void Endgame()
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
            lblPlayer1.Text = player[0].Name + "  " + player[0].Score;
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
            return Hangngang(btn)|| Hangdoc(btn) || Hangcheotrai(btn) || Hangcheophai(btn);


        }
        

        private Point Toado(Button btn)
        {
           
            int hangngang = Convert.ToInt32(btn.Tag);
            int hangdoc = Matrix[hangngang].IndexOf(btn);
            Point point = new Point(hangdoc, hangngang);

            return point;
        }
        private bool Hangngang(Button btn)
        {
            Point point= Toado(btn);
            int countLeft = 0;
            for(int i = point.X; i >= 0; i--)
                {
                    if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                    {
                        countLeft++;
                    }
                     else
                         break;
                }

            int countRight = 0;
            for(int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++)
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
        private bool Hangdoc(Button btn)
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
        private bool Hangcheotrai(Button btn)
        {
            Point point = Toado(btn);
            int countTop = 0;
            for(int i = 0; i <= point.X;i++)
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
            for(int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
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
        private bool Hangcheophai(Button btn)
        {
            Point point = Toado(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X  + i > Cons.CHESS_BOARD_WIDTH || point.Y - i < 0)
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
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i <0 )
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

        private void Mark(Button btn )
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1;
        }
        private void Changer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name;
            PlayerMark.Image = Player[CurrentPlayer].Mark;
        }

        private void AITurn()
        {
            Button btn = Nuoccotot();

            if (btn != null)
            {
                Mark(btn);

                if (isEndgame(btn))
                {
                    Endgame();
                    return;
                }

                Changer();
            }
        }

        private Button Nuoccotot()
        {
            Button bestBtn = null;
            long maxPoint = -1;

            // Nếu là nước đi đầu tiên của AI và bàn cờ trống, đánh vào giữa bàn cờ
            bool isFirstMove = true;

            foreach (var row in Matrix)
            {
                foreach (Button btn in row)
                {
                    if (btn.BackgroundImage != null)
                    {
                        isFirstMove = false;
                        break;
                    }
                }
                if (!isFirstMove) break;
            }

            if (isFirstMove)
            {
                return Matrix[Cons.CHESS_BOARD_HEIGHT / 2][Cons.CHESS_BOARD_WIDTH / 2];
            }

            // Tìm nước đi tốt nhất
            foreach (var row in Matrix)
            {
                foreach (Button btn in row)
                {
                    if (btn.BackgroundImage == null)
                    {
                        long point = Danhgianuocco(btn);

                        if (maxPoint == -1 || point > maxPoint)
                        {
                            maxPoint = point;
                            bestBtn = btn;
                        }
                    }
                }
            }

            return bestBtn;
        }

        private long Danhgianuocco(Button btn)
        {
            long attack = AttackPoint(btn);
            long defend = DefendPoint(btn);

            // Ưu tiên dứt điểm hoặc chặn đối thủ khi đã có 4 quân
            if (attack >= attackScore[4]) return attack * 10;
            if (defend >= defenseScore[4]) return defend * 10;

            // Tổng hợp cả điểm tấn công và phòng thủ để AI biết chọn nước đi "lưỡng toàn"
            return attack + defend;
        }

        private long AttackPoint(Button btn)
        {
            Image aiMark = Player[CurrentPlayer].Mark;
            long score = 0;
            score += GetDirectionScore(btn, 1, 0, aiMark, attackScore);  // Ngang
            score += GetDirectionScore(btn, 0, 1, aiMark, attackScore);  // Dọc
            score += GetDirectionScore(btn, 1, 1, aiMark, attackScore);  // Chéo chính \
            score += GetDirectionScore(btn, 1, -1, aiMark, attackScore); // Chéo phụ /
            return score;
        }

        private long DefendPoint(Button btn)
        {
            // Xác định quân của người chơi
            Image playerMark = Player[CurrentPlayer == 1 ? 0 : 1].Mark;
            long score = 0;
            score += GetDirectionScore(btn, 1, 0, playerMark, defenseScore);
            score += GetDirectionScore(btn, 0, 1, playerMark, defenseScore);
            score += GetDirectionScore(btn, 1, 1, playerMark, defenseScore);
            score += GetDirectionScore(btn, 1, -1, playerMark, defenseScore);
            return score;
        }

        private long GetDirectionScore(Button btn, int dx, int dy, Image targetMark, int[] scoreArray)
        {
            Point point = Toado(btn);
            int consecutive = 0; // Số quân liên tiếp
            int blocks = 0;      // Số đầu bị chặn

            // Kiểm tra chiều dương của vector (dx, dy)
            for (int i = 1; i <= 4; i++)
            {
                int nx = point.X + i * dx;
                int ny = point.Y + i * dy;

                if (nx < 0 || nx >= Cons.CHESS_BOARD_WIDTH || ny < 0 || ny >= Cons.CHESS_BOARD_HEIGHT)
                {
                    blocks++; // Chạm biên bàn cờ -> Bị chặn
                    break;
                }

                if (Matrix[ny][nx].BackgroundImage == targetMark)
                {
                    consecutive++;
                }
                else if (Matrix[ny][nx].BackgroundImage != null)
                {
                    blocks++; // Gặp quân của đối phương -> Bị chặn
                    break;
                }
                else
                {
                    break; // Gặp ô trống
                }
            }

            // Kiểm tra chiều âm của vector (-dx, -dy)
            for (int i = 1; i <= 4; i++)
            {
                int nx = point.X - i * dx;
                int ny = point.Y - i * dy;

                if (nx < 0 || nx >= Cons.CHESS_BOARD_WIDTH || ny < 0 || ny >= Cons.CHESS_BOARD_HEIGHT)
                {
                    blocks++; // Chạm biên
                    break;
                }

                if (Matrix[ny][nx].BackgroundImage == targetMark)
                {
                    consecutive++;
                }
                else if (Matrix[ny][nx].BackgroundImage != null)
                {
                    blocks++; // Bị chặn
                    break;
                }
                else
                {
                    break; // Gặp ô trống
                }
            }

            if (blocks == 2) return 0; // Bị chặn cả 2 đầu, nước đi này vô dụng

            if (consecutive >= 5) consecutive = 5; // Giới hạn index cho mảng điểm

            long score = scoreArray[consecutive];

            // Nếu nước đi mở 2 đầu (không bị chặn), tăng gấp đôi giá trị để ưu tiên
            if (blocks == 0 && consecutive > 0)
            {
                score *= 2;
            }

            return score;
        }



    }
    
    #endregion
}