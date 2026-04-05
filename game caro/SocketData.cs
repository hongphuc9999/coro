using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_caro
{
    [Serializable]
    internal class SocketData
    {
        private int commad;

        public int Commad
        { get { return commad; } set { commad = value; } }

        private Point point;

        public Point Point { get { return point; } set { point = value; } }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public SocketData(int commad, string message ,  Point point )
        {
            this.Commad = commad;
            this.Point = point;
        }
        public enum SocketCommad
        {
            SEND_POINT,
            NOTIFY,
              NEW_GAME,
            WIN,
            DRAW,
              UNDO,
              QUIT
        }
    }
}
