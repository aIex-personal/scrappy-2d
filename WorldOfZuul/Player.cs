using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Player
    {
        private int health;
        public int GetHealth()
        { return health; }
        public void SetHealth(int value)
        {
            this.health = value;
        }
        public Player()
        {
           health = -1;
        }
    }
}
