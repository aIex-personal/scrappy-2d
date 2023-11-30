using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.GameElements
{
    public class Inventory
    {
        private List<string> items;
        public Inventory()
        {
            items = new List<string>();
        }
        public List<string> ReadAll()
        {
            return items;
        }
        public int ItemsCount()
        {
            return items.Count;
        }
        public void ReadAllConsole()
        {
            foreach (var item in items) { Console.WriteLine("item"); }
        }
        public void AddAndSet(string item)
        {
            items.Add(item);
        }
        public void Drop(string item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }
        }
    }
}
