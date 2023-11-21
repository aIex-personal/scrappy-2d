using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.GameElements
{
    public class Player
    {
        private int health;
        public Inventory inventory;
        public int GetHealth()
        { return health; }
        public void SetHealth(int value)
        {
            health = value;
        }
        public Player()
        {
            health = -1;
            inventory = new Inventory();
        }
    }
}
