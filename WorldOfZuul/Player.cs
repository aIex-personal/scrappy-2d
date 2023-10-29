using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Player
    {
        public int health { get; set; }
        public Player(int health)
        {
                this.health = health;
        }
    }
}
