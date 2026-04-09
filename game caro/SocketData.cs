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
        public int Commad { get; set; }
        public Point Point { get; set; }
        public string Message { get; set; }

        public SocketData(int commad, string message, Point point)
        {
            Commad = commad;
            Message = message;
            Point = point;
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
