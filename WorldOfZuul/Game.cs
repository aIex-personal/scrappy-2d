namespace WorldOfZuul
{
    public class Game
    {
        private Room? currentRoom;
        private Room? previousRoom;

        public static int difficulty = 0; //This variable will store the difficulty level
                                          //Initalized with a 0, Easy = 1, Medium = 2, Hard = 3

        public Game()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {

            #region OriginalRooms
            //Room? outside = new("Outside", "You are standing outside the main entrance of the university. To the east is a large building, to the south is a computing lab, and to the west is the campus pub.");
            //Room? theatre = new("Theatre", "You find yourself inside a large lecture theatre. Rows of seats ascend up to the back, and there's a podium at the front. It's quite dark and quiet.");
            //Room? pub = new("Pub", "You've entered the campus pub. It's a cozy place, with a few students chatting over drinks. There's a bar near you and some pool tables at the far end.");
            //Room? lab = new("Lab", "You're in a computing lab. Desks with computers line the walls, and there's an office to the east. The hum of machines fills the room.");
            //Room? office = new("Office", "You've entered what seems to be an administration office. There's a large desk with a computer on it, and some bookshelves lining one wall.");

            //outside.SetExits(null, theatre, lab, pub); // North, East, South, West

            //theatre.SetExit("west", outside);

            //pub.SetExit("east", outside);

            //lab.SetExits(outside, office, null, null);

            //office.SetExit("west", lab);

            //currentRoom = outside;
            #endregion


            //Creating the rooms, in the constructor there is the name of the room, and after that there is a 
            //description with the possible doors to open. 
            Room? outside = new("Outside", "You are standing outside of a recycling centre. To the north, there seems to be an entrance to the building.");
            Room? hall = new("Hall", "You find yourself in the hall. To the south is your exit from the building, and there are signs above the other doors. To the West, there seems to be a sorting room, to the North it says E-waste recycling, and to the East, the sign says Paper mill.");
            Room? sortingRoom = new("Sorting Room", "It seems that people in this room are separating garbages. They have different places to collect Paper, Plastic, Organic Junk and Metal. To the North, there is a door that says Upcycling Studio, and to the East there is the Hall");
            Room? plasticRecycling = new("Plastic Recycling", "In this room people seem to know a lot about the recycling of plastic. To the North, there is a room called Paper Mill, and to the West there is the hall.");
            Room? upcyclingStudio = new("Upcycling Studio", "In this room people are creating new things out of old, battle-worn things. The door to the West seems to be an exit to a Composting Garden, the sign on the door to the North says Ocean Cleanup, to the East there is Recycled Art Gallery, and to the South there is a Sorting Room.");
            Room? compostingGarden = new("Composting Garden", "In this garden there are loads of used Organical stuff, like the skins of vegetables, the remains of fruits etc. The Garden is surrounded by hedge, so the only entrance is to the East, back to the Upcycling Studio.");
            Room? eWasteRecycling = new("E-Waste Recycling", "In this Room people are repairing old electrical stuff. To the North, there is a Recycled art gallery, to the East there is a Paper Mill, to the South there is the Hall and to the West there is an Upcycling Studio");
            Room? paperMill = new("Paper Mill","In this room people know a lot about recycling paper. To the North there is a room related to planting trees, to the South there is a room about Plastic Recycling and to the West there is a room about recycling e-waste.");
            Room? oceanCleanup = new("Ocean Cleanup", "People are thinking about solutions for cleaning the ocean from garbage. To the East there is a Recycled art Gallery and to the South there is an Upcycling Studio");
            Room? recycledArtGallery = new("Recycled Art Gallery", "This room seems to be a museum about great actions of recyclement from all around the world. To the North, there is a locked door, to the East there is a room about Planting trees, to the South there is a room about E-waste and to the East there is a room related to cleaning the Oceans ");
            Room? plantingTrees = new("Planting Trees", "People here are looking for the best way to plant more and more trees all around the world. To the South there is a Paper Mill and to the West there is a Recycled Art Gallery.");
            Room? mysteryRoom = new("Final Mission Room", "WOW");

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

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentRoom?.ShortDescription);
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

                    default:
                        Console.WriteLine("I don't know what command.");
                        break;
                }
            }

            Console.WriteLine("Thank you for playing World of Zuul!");
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
            Console.WriteLine("Welcome to THE WAY BACK HOME: A recycling adventure!");
            Console.WriteLine();
            Console.WriteLine("Press Space to continue");
            Console.ReadKey();   //You can press any key and the program will go on
            Console.Clear();
            
            //Selecting difficulty level
            while(true) 
            {
                Console.WriteLine("Select your difficulty!");
                Console.WriteLine("         _________");
                Console.WriteLine("  Easy   OO");
                Console.WriteLine("         ¯¯¯¯¯¯¯¯¯");
                Console.WriteLine("         _________");
                Console.WriteLine("  Medium OOOOO");
                Console.WriteLine("         ¯¯¯¯¯¯¯¯¯");
                Console.WriteLine("         _________");
                Console.WriteLine("  Hard   OOOOOOOOO");
                Console.WriteLine("         ¯¯¯¯¯¯¯¯¯");
                Console.WriteLine("Difficulty level affects your health level, and the difficulty of the missions");
                string? diff = Console.ReadLine();   //Question mark will prevent the variable to get a null value

                //Setting the difficulty variable
                if (diff?.ToLower() == "easy")  //.ToLower is needed, so the player can type with or without capital letters
                {
                    difficulty = 1;
                    break;  //breaks/leaves the while cycle
                }
                else if (diff?.ToLower() == "medium")
                {
                    difficulty = 2;
                    break;
                }
                else if (diff?.ToLower() == "hard")
                {
                    difficulty = 3;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input, type 'EASY', 'MEDIUM', or 'HARD'!");
                } //Will continue until the user types easy, medium or hard
            }
            Console.Clear();
            Console.WriteLine("Let's start this adventure!");
            Console.WriteLine();
            Console.WriteLine("Press Space to start");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Intro text.");
            Console.Clear();
            //Console.ReadKey();
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous room.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
        }
    }
}
