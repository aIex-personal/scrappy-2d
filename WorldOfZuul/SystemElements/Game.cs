using ConsoleClient.GameElements;
using System;
using System.Net.NetworkInformation;
using System.Security;
using System.Threading.Tasks.Dataflow;

namespace ConsoleClient.SystemElements
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;

        public static int difficulty = 0; //This variable will store the difficulty level
                                          //Initalized with a 0, Easy = 1, Medium = 2, Hard = 3

        public static Player Scrappy;
        //private int clearCounter;

        public Game()
        {
            Scrappy = new Player();
            //this.clearCounter = 0;
            CreateRooms();
        }

        private void CreateRooms()
        {
            #region oldCode
            //Creating the rooms, in the constructor there is the name of the room, and after that there is a 
            //description with the possible doors to open. 
            //        Room? outside = new("Outside", 
            //            "You are standing outside of a recycling centre. To the north, there seems to be an entrance to the building.",
            //            "First Time at outside");
            //        Room? hall = new("Hall", 
            //            @"
            //You find yourself in the hall. To the south is your exit from the building, 
            //and there are signs above the other doors. To the West, there seems to be a sorting room,
            //to the North it says E-waste recycling, and to the East, the sign says Paper mill.", 
            //            "First time at the Hall");
            //        Room? sortingRoom = new("Sorting Room", "It seems that people in this room are separating garbages. They have different places to collect Paper, Plastic, Organic Junk and Metal. To the North, there is a door that says Upcycling Studio, and to the East there is the Hall",
            //            "First time at the sorting Room");
            //        Room? plasticRecycling = new("Plastic Recycling", "In this room people seem to know a lot about the recycling of plastic. To the North, there is a room called Paper Mill, and to the West there is the hall.",
            //            "First time at the PlasticRecyclingRoom");
            //        Room? upcyclingStudio = new("Upcycling Studio", "In this room people are creating new things out of old, battle-worn things. The door to the West seems to be an exit to a Composting Garden, the sign on the door to the North says Ocean Cleanup, to the East there is Recycled Art Gallery, and to the South there is a Sorting Room.",
            //            "First time at the UpcyclingStudio");
            //        Room? compostingGarden = new("Composting Garden", "In this garden there are loads of used Organical stuff, like the skins of vegetables, the remains of fruits etc. The Garden is surrounded by hedge, so the only entrance is to the East, back to the Upcycling Studio.",
            //            "First time at the CompostingGarden");
            //        Room? eWasteRecycling = new("E-Waste Recycling", "In this Room people are repairing old electrical stuff. To the North, there is a Recycled art gallery, to the East there is a Paper Mill, to the South there is the Hall and to the West there is an Upcycling Studio",
            //            "First time at the eWasteRecycling");
            //        Room? paperMill = new("Paper Mill","In this room people know a lot about recycling paper. To the North there is a room related to planting trees, to the South there is a room about Plastic Recycling and to the West there is a room about recycling e-waste.",
            //            "First time at the paperMill");
            //        Room? oceanCleanup = new("Ocean Cleanup", "People are thinking about solutions for cleaning the ocean from garbage. To the East there is a Recycled art Gallery and to the South there is an Upcycling Studio",
            //            "First time at the oceanCleanupRoom");
            //        Room? recycledArtGallery = new("Recycled Art Gallery", "This room seems to be a museum about great actions of recyclement from all around the world. To the North, there is a locked door, to the East there is a room about Planting trees, to the South there is a room about E-waste and to the East there is a room related to cleaning the Oceans ",
            //            "First time at the recycledArtGallery");
            //        Room? plantingTrees = new("Planting Trees", "People here are looking for the best way to plant more and more trees all around the world. To the South there is a Paper Mill and to the West there is a Recycled Art Gallery.",
            //            "First time at the plantingTreesRoom");
            //        FinalRoom? mysteryRoom = new("Final Mission Room", "WOW", "First time at the mysteryRoom");

            //        //north, east, south, west
            //        //Setting the relations between rooms. 
            //        outside.SetExit("north", hall);
            //        hall.SetExits(eWasteRecycling, plasticRecycling, outside, sortingRoom);
            //        sortingRoom.SetExits(upcyclingStudio, hall, null, null);
            //        plasticRecycling.SetExits(paperMill, null, null, hall);
            //        upcyclingStudio.SetExits(oceanCleanup, eWasteRecycling, sortingRoom, compostingGarden);
            //        compostingGarden.SetExit("east", upcyclingStudio);
            //        eWasteRecycling.SetExits(recycledArtGallery, paperMill, hall, upcyclingStudio);
            //        paperMill.SetExits(plantingTrees, null, plasticRecycling, eWasteRecycling);
            //        oceanCleanup.SetExits(null, recycledArtGallery, upcyclingStudio, null);
            //        recycledArtGallery.SetExits(mysteryRoom, plantingTrees, eWasteRecycling, oceanCleanup);
            //        plantingTrees.SetExits(null, null, paperMill, recycledArtGallery);
            //        mysteryRoom.SetExit("south", recycledArtGallery);

            //        //Starting  
            //        currentRoom = outside;
            #endregion
            Room outside = new("Outside", @"First time of Scrappy looking around outside the Recycling Centre, his ship is broken.
There is a door to the North, the sign says Hall.",
@"Visiting outside, The ship still needs to be repaired, to the North there is the Hall");
            Room hall = new("Hall", @"First time of Scrappy is inside of the Recycling Centre, there is a kind robot.
There is a door to the North, the sign doesn't say anything.
To the West, there is a composting centre, to the East there is a recycling room",
                                           @"You are in the hall, to the North there is a mysterious room, to the East there is 
a recycling room, to the West there is the composting room, and outside is to the South. ");
            Room compost = new("Composting Garden", @"First Time Scrapp at composting garden. East: Hall",
                                          @"East: Hall");
            Room recyclingRoom = new("Recycling Room", @"First Time at Recycling Room. West: Hall",
                                          @"Recycling Room. West: Hall");
            FinalRoom mysteryRoom = new FinalRoom("Final mission Room", "First Time at Final mission Room blablabla",
                                                "You have accomplished your mission. You have no things left to do here");

            outside.SetExit("north", hall);
            hall.SetExits(mysteryRoom, recyclingRoom, outside, compost);
            recyclingRoom.SetExit("west", hall);
            compost.SetExit("east", hall);
            mysteryRoom.SetExit("south", hall);
            currentRoom = outside;
        }

        public void Play()
        {
            Parser parser = new();
            PrintWelcome();
            PrintPrologue();
            Console.WriteLine($"Scrappy's Health level: {Scrappy.GetHealth}");
            Console.WriteLine();
            //PrintHelp();

            bool continuePlaying = true;
            while (continuePlaying)
            {

                Console.WriteLine();
                if (currentRoom.FirstEnter)
                {
                    TypeLine(currentRoom.FirstDescription);
                    currentRoom.SetFirstEnterFalse();
                    Console.ReadKey();
                }
                else
                {
                    TypeLine(currentRoom?.ShortDescription);
                    Console.ReadKey();
                }

                Console.Write("> ");

                string[] commands = new string[] { "NORTH ", "EAST  ", "SOUTH ", "WEST  ",
                    "LOOK  ", "BACK  ", "QUIT  ", "HELP  "};

                MenuPlain commandMenu = new MenuPlain(
                    "\r\nNavigate by choosing 'NORTH', 'SOUTH', 'EAST', or 'WEST'" +
                    "\r\nChoose LOOK for more details" +
                    "\r\nChoose BACK to go to the previous room" +
                    "\r\nChoose HELP to print this message again" +
                    "\r\nChoose QUEST to do this room's quest" +
                    "\r\nChosse QUIT to exit the game" +
                    "\r\n ", commands);
                int commandIndex = commandMenu.Run();

                string? input = commands[commandIndex].ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    TypeLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    TypeLine("I don't know that command.");
                    continue;
                }
                switch (command.Name)
                {
                    case "look":
                        TypeLine(currentRoom?.LongDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                        {
                            TypeLine("You can't go back from here!");
                            Console.ReadKey();
                        }
                        else
                            currentRoom = previousRoom;
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        if (currentRoom is FinalRoom)
                        {
                            if ((currentRoom as FinalRoom).CanEnter() == true)
                            {
                                Move(command.Name);
                                break;
                            }
                            else
                            {
                                TypeLine("You don't have all the items to enter the Room!");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Move(command.Name);
                        }

                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;
                    //case "map":
                    //    PrintMinimap();
                    //    break;
                    default:
                        TypeLine("I don't know what command.");
                        Console.ReadKey();
                        break;
                }
            }

            TypeLine("Thank you for playing THE WAY BACK HOME: A recycling adventure!!");
        }

        private void Move(string direction)
        {
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                previousRoom = currentRoom;
                currentRoom = currentRoom?.Exits[direction];
            }
            else
            {
                TypeLine($"You can't go {direction}!");
                Console.ReadKey();
            }
        }

        private static void PrintWelcome()
        {
            Console.SetWindowSize(175, 35);

            Console.WriteLine(@"

                ████████╗██╗  ██╗███████╗    ██╗    ██╗ █████╗ ██╗   ██╗    ██████╗  █████╗  ██████╗██╗  ██╗    ██╗  ██╗ ██████╗ ███╗   ███╗███████╗                       
                ╚══██╔══╝██║  ██║██╔════╝    ██║    ██║██╔══██╗╚██╗ ██╔╝    ██╔══██╗██╔══██╗██╔════╝██║ ██╔╝    ██║  ██║██╔═══██╗████╗ ████║██╔════╝██╗                    
                   ██║   ███████║█████╗      ██║ █╗ ██║███████║ ╚████╔╝     ██████╔╝███████║██║     █████╔╝     ███████║██║   ██║██╔████╔██║█████╗  ╚═╝                    
                   ██║   ██╔══██║██╔══╝      ██║███╗██║██╔══██║  ╚██╔╝      ██╔══██╗██╔══██║██║     ██╔═██╗     ██╔══██║██║   ██║██║╚██╔╝██║██╔══╝  ██╗                    
                   ██║   ██║  ██║███████╗    ╚███╔███╔╝██║  ██║   ██║       ██████╔╝██║  ██║╚██████╗██║  ██╗    ██║  ██║╚██████╔╝██║ ╚═╝ ██║███████╗╚═╝                    
                   ╚═╝   ╚═╝  ╚═╝╚══════╝     ╚══╝╚══╝ ╚═╝  ╚═╝   ╚═╝       ╚═════╝ ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝    ╚═╝  ╚═╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝                       
     █████╗     ██████╗ ███████╗ ██████╗██╗   ██╗ ██████╗██╗     ██╗███╗   ██╗ ██████╗      █████╗ ██████╗ ██╗   ██╗███████╗███╗   ██╗████████╗██╗   ██╗██████╗ ███████╗██╗
    ██╔══██╗    ██╔══██╗██╔════╝██╔════╝╚██╗ ██╔╝██╔════╝██║     ██║████╗  ██║██╔════╝     ██╔══██╗██╔══██╗██║   ██║██╔════╝████╗  ██║╚══██╔══╝██║   ██║██╔══██╗██╔════╝██║
    ███████║    ██████╔╝█████╗  ██║      ╚████╔╝ ██║     ██║     ██║██╔██╗ ██║██║  ███╗    ███████║██║  ██║██║   ██║█████╗  ██╔██╗ ██║   ██║   ██║   ██║██████╔╝█████╗  ██║
    ██╔══██║    ██╔══██╗██╔══╝  ██║       ╚██╔╝  ██║     ██║     ██║██║╚██╗██║██║   ██║    ██╔══██║██║  ██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║   ██║   ██║   ██║██╔══██╗██╔══╝  ╚═╝
    ██║  ██║    ██║  ██║███████╗╚██████╗   ██║   ╚██████╗███████╗██║██║ ╚████║╚██████╔╝    ██║  ██║██████╔╝ ╚████╔╝ ███████╗██║ ╚████║   ██║   ╚██████╔╝██║  ██║███████╗██╗
    ╚═╝  ╚═╝    ╚═╝  ╚═╝╚══════╝ ╚═════╝   ╚═╝    ╚═════╝╚══════╝╚═╝╚═╝  ╚═══╝ ╚═════╝     ╚═╝  ╚═╝╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═══╝   ╚═╝    ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝
");
            Console.WriteLine();
            Console.WriteLine("Press Space to continue");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }  //Player can only proceed in the menu with spacebar




            //Selecting difficulty level
            while (true)
            {
                string prompt = "Select your difficulty!";
                string[] options = { " Easy ", "Medium", " Hard " };
                string[] difficulties = { "|   |===            |  |", "|   |========       |  |", "|   |===============|  |" };
                Menu DifficultyMenu = new DifficultyMenu(prompt, options, difficulties);
                int selectedIndex = DifficultyMenu.Run();

                string? diff = options[selectedIndex];   //Question mark will prevent the variable to get a null value

                #region setting diffculty level variable
                //Setting the difficulty variable
                if (diff == options[0])  //.ToLower is needed, so the player can type with or without capital letters
                {
                    difficulty = 1;
                    Scrappy.SetHealth(10);
                    break;  //breaks/leaves the while cycle
                }
                else if (diff == options[1])
                {
                    difficulty = 2;
                    Scrappy.SetHealth(6);
                    break;
                }
                else if (diff == options[2])
                {
                    difficulty = 3;
                    Scrappy.SetHealth(3);
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input, type 'EASY', 'MEDIUM', or 'HARD'!");
                } //Will continue until the user types easy, medium or hard
                #endregion
            }
            Console.Clear();
            //Console.WriteLine("Let's start this adventure!");
            //Console.WriteLine();
            //Console.WriteLine("Press Space to start");
            //Console.ReadKey();
            //Console.Clear();
            //Console.WriteLine("Intro text.");
            //Console.Clear();
            //Console.ReadKey();

            //PrintHelp();
            Console.WriteLine();
        }
        private static void PrintPrologue()
        {
            Menu menu = new Menu("Prologue", new string[] { "Play", "Skip" });
            int selectedIndex = menu.Run();
            if (selectedIndex == 0)
            {
                Console.Clear();
                string[] lines = new string[4];
                lines[0] = "\t\t\t\t\t\t" + " In the far reaches of the galaxy, a determined alien named Scrappy" + "\r\n" +
                           "\t\t\t\t\t\t" + "  embarked on a journey through space. Their home world was a dire " + "\r\n" +
                           "\t\t\t\t\t\t" + " place, suffering from an ecological catastrophe. The air was thick " + "\r\n" +
                           "\t\t\t\t\t\t" + "with pollution, and vast mountains of garbage littered the landscape. " + "\r\n" +
                           "\t\t\t\t\t\t" + "  It was a world without recycling, and the consequences were dire." + "\r\n";
                lines[1] = "\t\t\t\t\t\t" + "     Scrappy had constructed a spaceship from the remnants of their " + "\r\n" +
                           "\t\t\t\t\t\t" + "       once-advanced civilization, launching it into the cosmos. " + "\r\n" +
                           "\t\t\t\t\t\t" + "     As they traveled through the stars, their fuel gauge began to " + "\r\n" +
                           "\t\t\t\t\t\t" + " flicker ominously, a reminder of the need for a new source of energy." + "\r\n";
                lines[2] = "\t\t\t\t\t\t" + " Earth, a distant blue planet, came into view. Its vibrant surface" + "\r\n" +
                           "\t\t\t\t\t\t" + "  hinted at abundant resources that might hold the key to saving " + "\r\n" +
                           "\t\t\t\t\t\t" + "     Scrappy's world. Landing on Earth marked the start of an  " + "\r\n" +
                           "\t\t\t\t\t\t" + " epic adventure, a quest to find fuel for the spaceship. Scrappy was " + "\r\n" +
                           "\t\t\t\t\t\t" + " amazed by how little garbage there was on Earth, a stark contrast " + "\r\n" +
                           "\t\t\t\t\t\t" + "   to the pollution and waste that had engulfed their own planet.";
                lines[3] = "\t\t\t\t\t\t" + "       With each step, the fate of Scrappy's world and the " + "\r\n" +
                           "\t\t\t\t\t\t" + "   promise of Earth became intertwined, setting the stage for an " + "\r\n" +
                           "\t\t\t\t\t\t" + "       interstellar mission filled with hope and discovery.";
                for (int i = 0; i < lines.Length; i++)
                {
                    TypeLinePrologue(lines[i]);
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
            }
        }
        private static void TypeLinePrologue(string line)
        {
            Console.SetCursorPosition(0, 10);
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(1);
            }
        }
        private static void TypeLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(1);
            }
        }
        private static void PrintHelp()
        {
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'map' to print the minimap");
            Console.WriteLine("Tpye 'quest' to do this room's quest.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
        //private static void PrintMinimap()
        //{

        //    Console.Clear();
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("=========================================================");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine("                             |                           ");
        //    Console.WriteLine($"                   {outside.MinimapDefault}                     ");
        //    Console.WriteLine("Press any key to return");
        //}


    }
}



