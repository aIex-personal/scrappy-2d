using System;
using System.Collections.Generic;

namespace ConsoleClient.GameElements
{
    public class Room
    {
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public string FirstDescription { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; } = new();

        private bool firstEnter;
        public bool FirstEnter
        {
            get { return firstEnter; }
        }
        public void SetFirstEnterFalse()
        {
            firstEnter = false;
        }

        public Room(string shortDesc, string firstDescription, string longDesc)
        {
            ShortDescription = shortDesc;
            LongDescription = longDesc;
            firstEnter = true;
            FirstDescription = firstDescription;
        }

        public void SetExits(Room? north, Room? east, Room? south, Room? west)
        {
            SetExit("north", north);
            SetExit("east", east);
            SetExit("south", south);
            SetExit("west", west);
        }

        public void SetExit(string direction, Room? neighbor)
        {
            if (neighbor != null)
                Exits[direction] = neighbor;
        }
    }
}
