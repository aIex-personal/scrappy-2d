using ConsoleClient;
using System;
using System.Security;
using System.Threading.Tasks.Dataflow;

namespace WorldOfZuul
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;

        public static int difficulty = 0; //This variable will store the difficulty level
                                          //Initalized with a 0, Easy = 1, Medium = 2, Hard = 3

        public static Player Scrappy;
        private int clearCounter;

        public Game()
        {
            Scrappy = new Player();
            this.clearCounter = 0;
            CreateRooms();
        }

        private void CreateRooms()
        {
            //Creating the rooms, in the constructor there is the name of the room, and after that there is a 
            //description with the possible doors to open. 
            Room? outside = new("Outside", 
                "You are standing outside of a recycling centre. To the north, there seems to be an entrance to the building.",
                "First Time at outside");
            Room? hall = new("Hall", 
                @"
    You find yourself in the hall. To the south is your exit from the building, 
    and there are signs above the other doors. To the West, there seems to be a sorting room,
    to the North it says E-waste recycling, and to the East, the sign says Paper mill.", 
                "First time at the Hall");
            Room? sortingRoom = new("Sorting Room", "It seems that people in this room are separating garbages. They have different places to collect Paper, Plastic, Organic Junk and Metal. To the North, there is a door that says Upcycling Studio, and to the East there is the Hall",
                "First time at the sorting Room");
            Room? plasticRecycling = new("Plastic Recycling", "In this room people seem to know a lot about the recycling of plastic. To the North, there is a room called Paper Mill, and to the West there is the hall.",
                "First time at the PlasticRecyclingRoom");
            Room? upcyclingStudio = new("Upcycling Studio", "In this room people are creating new things out of old, battle-worn things. The door to the West seems to be an exit to a Composting Garden, the sign on the door to the North says Ocean Cleanup, to the East there is Recycled Art Gallery, and to the South there is a Sorting Room.",
                "First time at the UpcyclingStudio");
            Room? compostingGarden = new("Composting Garden", "In this garden there are loads of used Organical stuff, like the skins of vegetables, the remains of fruits etc. The Garden is surrounded by hedge, so the only entrance is to the East, back to the Upcycling Studio.",
                "First time at the CompostingGarden");
            Room? eWasteRecycling = new("E-Waste Recycling", "In this Room people are repairing old electrical stuff. To the North, there is a Recycled art gallery, to the East there is a Paper Mill, to the South there is the Hall and to the West there is an Upcycling Studio",
                "First time at the eWasteRecycling");
            Room? paperMill = new("Paper Mill","In this room people know a lot about recycling paper. To the North there is a room related to planting trees, to the South there is a room about Plastic Recycling and to the West there is a room about recycling e-waste.",
                "First time at the paperMill");
            Room? oceanCleanup = new("Ocean Cleanup", "People are thinking about solutions for cleaning the ocean from garbage. To the East there is a Recycled art Gallery and to the South there is an Upcycling Studio",
                "First time at the oceanCleanupRoom");
            Room? recycledArtGallery = new("Recycled Art Gallery", "This room seems to be a museum about great actions of recyclement from all around the world. To the North, there is a locked door, to the East there is a room about Planting trees, to the South there is a room about E-waste and to the East there is a room related to cleaning the Oceans ",
                "First time at the recycledArtGallery");
            Room? plantingTrees = new("Planting Trees", "People here are looking for the best way to plant more and more trees all around the world. To the South there is a Paper Mill and to the West there is a Recycled Art Gallery.",
                "First time at the plantingTreesRoom");
            FinalRoom? mysteryRoom = new("Final Mission Room", "WOW", "First time at the mysteryRoom");

            //north, east, south, west
            //Setting the relations between rooms. 
            outside.SetExit("north", hall);
            hall.SetExits(eWasteRecycling, plasticRecycling, outside, sortingRoom);
            sortingRoom.SetExits(upcyclingStudio, hall, null, null);
            plasticRecycling.SetExits(paperMill, null, null, hall);
            upcyclingStudio.SetExits(oceanCleanup, eWasteRecycling, sortingRoom, compostingGarden);
            compostingGarden.SetExit("east", upcyclingStudio);
            eWasteRecycling.SetExits(recycledArtGallery, paperMill, hall, upcyclingStudio);
            paperMill.SetExits(plantingTrees, null, plasticRecycling, eWasteRecycling);
            oceanCleanup.SetExits(null, recycledArtGallery, upcyclingStudio, null);
            recycledArtGallery.SetExits(mysteryRoom, plantingTrees, eWasteRecycling, oceanCleanup);
            plantingTrees.SetExits(null, null, paperMill, recycledArtGallery);
            mysteryRoom.SetExit("south", recycledArtGallery);

            //Starting room
            currentRoom = outside;

        }

        public void Play()
        {
            Parser parser = new();
            PrintWelcome();
            PrintPrologue();
            Console.WriteLine($"Scrappy's Health level: {Scrappy.GetHealth}");
            Console.WriteLine();
            PrintHelp();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                 
                Console.WriteLine();                                             
                if (currentRoom.FirstEnter)
                {
                    Console.WriteLine(currentRoom.FirstDescription);
                    currentRoom.SetFirstEnterFalse();
                }
                else
                {
                    Console.WriteLine(currentRoom?.ShortDescription);
                }

                Console.Write("> ");
                
                string? input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }
                if (clearCounter == 2)
                {
                    Console.Clear();
                    Console.WriteLine($"Scrappy's Health level: {Scrappy.GetHealth}");
                    Console.WriteLine();
                    PrintHelp();
                    Console.WriteLine();
                    clearCounter = 0;
                }
                else
                {
                    clearCounter++;
                }
                
                switch(command.Name)
                {
                    case "look":
                        Console.WriteLine(currentRoom?.LongDescription);
                        break;

                    case "back":
                        if (previousRoom == null)
                            Console.WriteLine("You can't go back from here!");
                        else
                            currentRoom = previousRoom;
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;
                    case "map":
                        PrintMinimap();
                        break;
                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing THE WAY BACK HOME: A recycling adventure!!");
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
                Console.WriteLine($"You can't go {direction}!");
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
            Console.WriteLine("Press Space to continue");
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar){ }  //Player can only proceed in the menu with spacebar
            



            //Selecting difficulty level
            while(true) 
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
            Menu menu = new Menu("Prologue",new string[] {"Play", "Skip"});
            int selectedIndex = menu.Run();
            if (selectedIndex == 0)
            {
                Console.Clear();
                string[] lines = new string[3];
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
                           "\t\t\t\t\t\t" + "with pollution, and vast mountains of garbage littered the landscape. " + "\r\n" +
                           "\t\t\t\t\t\t" + "  It was a world without recycling, and the consequences were dire." + "\r\n";
                for (int i = 0; i < lines.Length; i++)
                {
                    TypeLine(lines[i]);
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.Clear();
            }
        }
        private static void TypeLine(string line)
        {
            Console.SetCursorPosition(0,10);
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
            Console.WriteLine("Type 'quit' to exit the game.");
        }
        private static void PrintMinimap()
        {
            
            Console.Clear();
            Console.WriteLine("                                                |");
            Console.WriteLine("                                                |");
            Console.WriteLine("                                                |");
            Console.WriteLine("                     =======================================================");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("       =====================================================================");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     |                          |                          |");
            Console.WriteLine("                     =======================================================");
            Console.WriteLine("                                                |");
            Console.WriteLine("                                                |");
            Console.WriteLine("                                                |");
            Console.WriteLine();
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
            Console.Clear();
            PrintHelp();
        }


    }
}



