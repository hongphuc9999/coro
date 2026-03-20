using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_caro
{
     public  class Player
    {
        private string name;

        public string Name{
            get { return name; }
            set { name = value; }
        }
        private Image mark;
        public Image Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        public Player(string Name, Image Mark) { 
            this.name = Name;
            this.mark = Mark;
        }
        public int Score { get; set; }

    }
}
