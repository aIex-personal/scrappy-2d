using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.SystemElements
{
    public class CommandWords
    {
        public List<string> ValidCommands { get; } = new List<string> { "north", "east"
            , "south", "west", "look", "back", "quit", "help", "map", "quest", "quiz",
        "health", "read", "robot", "snake", "items", "sort", "leave"};

        public bool IsValidCommand(string command)
        {
            return ValidCommands.Contains(command);
        }
    }

}
