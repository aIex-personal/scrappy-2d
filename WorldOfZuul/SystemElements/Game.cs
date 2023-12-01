using ConsoleClient.GameElements;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleClient.SystemElements
{
    public class Game
    {
        //Initalizing Game's classes variables
        private Room? currentRoom;
        private Room? previousRoom;

        public static int difficulty = 0; //This variable will store the difficulty level
                                          //Initalized with a 0, Easy = 1, Medium = 2, Hard = 3

        public static Player Scrappy;
        //private int clearCounter;

        public Game() //constructor
        {
            Scrappy = new Player();
            CreateRooms();
        }

        private void CreateRooms() //Creating the Rooms, setting their exits and setting the starting room
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

//            but his ship is broken.
//He needs parts and fuel for his spaceship.
//Without them, Scrappy will be stuck on planet Earth,
//And his home planet will get destroyed from the waste for sure.
//Scrappy needs to act fast.
//There is a door to the North, the sign says Hall to the Recycling Centre.
//There is also a wooden board next to the door, with some writing on it.
            Room outside = new("Outside", @"Scrappy has landed his ship in front of a building, 
",
@"Visiting outside, The ship still needs to be repaired, to the North there is the Hall",
new MenuPlain(
                    "\r\n Outside" +
                    "\r\n" +
                    "\r\n LOOK for more details in the room" +
                    "\r\n BACK to go to the previous room" +
                    "\r\n HELP to print this message again" +
                    "\r\n HEALTH to see Scrappy's health level" +
                    "\r\n BOARD to read the wooden board's text" +
                    "\r\n QUIT to exit the game" +
                    "\r\n ", new string[] { "NORTH ", "LOOK  ", "BACK  ","HEALTH", "HELP  ", "READ  ", "QUIT  " }
                    )

);

            Room hall = new("Hall", @"
First time of Scrappy is inside of the Recycling Centre, there is a kind robot.
The robot might know some things about recycling, it would be wise to have a
conversation with it. 
There is a door to the North, the sign doesn't say anything.
The door seems to be locked for now. 
To the West, there seems to be a garden, 
and to the East the sign says: 'Recycling Room'",
                                           @"
You are in the hall, there is a robot standing behind the counter, 
to the North there is a mysterious room, to the East there is 
a recycling room, to the West there is the composting room and outside is to the South. ",
new MenuPlain(
                    "\r\n Hall" +
                    "\r\n" +
                    "\r\n LOOK for more details" +
                    "\r\n BACK to go to the previous room" +
                    "\r\n HELP to print this message again" +
                    "\r\n QUEST to do this room's quest" +
                    "\r\n QUIT to exit the game" +
                    "\r\n QUEST to do the quest in this room" +
                    "\r\n ", new string[] {"NORTH ", "EAST  ", "SOUTH ", "WEST  ",
                    "LOOK  ", "BACK  ","HELP  ","QUEST ", "QUIT  "}));

            Room compost = new("Composting Garden", @"First Time Scrapp at composting garden. East: Hall",
                                          @"East: Hall",
                                          new MenuPlain(
                    "\r\nComposting Garden" +
                    "\r\nNavigate by choosing 'NORTH', 'SOUTH', 'EAST', or 'WEST'" +
                    "\r\nChoose LOOK for more details" +
                    "\r\nChoose BACK to go to the previous room" +
                    "\r\nChoose HELP to print this message again" +
                    "\r\nChoose QUEST to do this room's quest" +
                    "\r\nChosse QUIT to exit the game" +
                    "\r\nChoose QUEST to do the quest in this room" +
                    "\r\n ", new string[] { "EAST  ", "LOOK  ", "BACK  ", "HELP  ", "QUEST ", "QUIT  " }));

            Room recyclingRoom = new("Recycling Room", @"First Time at Recycling Room. West: Hall",
                                          @"Recycling Room. West: Hall",
                                          new MenuPlain(
                    "\r\nRecycling Room" +
                    "\r\nNavigate by choosing 'NORTH', 'SOUTH', 'EAST', or 'WEST'" +
                    "\r\nChoose LOOK for more details" +
                    "\r\nChoose BACK to go to the previous room" +
                    "\r\nChoose HELP to print this message again" +
                    "\r\nChoose QUEST to do this room's quest" +
                    "\r\nChosse QUIT to exit the game" +
                    "\r\nChoose QUEST to do the quest in this room" +
                    "\r\n ", new string[] { "WEST  ", "LOOK  ", "BACK  ", "HELP  ", "QUEST ", "QUIT  " }));
            FinalRoom mysteryRoom = new FinalRoom("Final mission Room", "First Time at Final mission Room blablabla",
                                                "You have accomplished your mission. You have no things left to do here",
                                               new MenuPlain(
                    "\r\n Final Room" +
                    "\r\nNavigate by choosing 'NORTH', 'SOUTH', 'EAST', or 'WEST'" +
                    "\r\nChoose LOOK for more details" +
                    "\r\nChoose BACK to go to the previous room" +
                    "\r\nChoose HELP to print this message again" +
                    "\r\nChoose QUEST to do this room's quest" +
                    "\r\nChosse QUIT to exit the game" +
                    "\r\n ", new string[] {"SOUTH ", "LOOK  ", "BACK  ","HELP  ","QUEST ","QUIZ  ", "QUIT  "}));

            
            outside.SetExit("north", hall);
            hall.SetExits(mysteryRoom, recyclingRoom, outside, compost);
            recyclingRoom.SetExit("west", hall);
            compost.SetExit("east", hall);
            mysteryRoom.SetExit("south", hall);
            currentRoom = outside;
        }   

        public void Play() //Playing the Game
        {
            Parser parser = new();
            PrintWelcome();
            //PrintPrologue();

            string commandString;
            //int commandIndex = -1;
            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine();
                //Checking If this is the first time the player is entering this room
                if (currentRoom.FirstEnter)
                {
                    //If yes, write the Room's first description, and set the room's FirstEnter prop to false
                    Console.Clear();
                    TypeLine(currentRoom.FirstDescription);
                    Console.ReadKey();
                    currentRoom.SetFirstEnterFalse();
                    commandString = currentRoom.commandMenu.Run();
                }
                else
                {
                    //If no, just write the default description of the room. 
                    //Console.Clear();
                    commandString = currentRoom.commandMenu.Run();
                    //TypeLine(currentRoom?.LongDescription);
                    //Console.ReadKey();
                }
                //Checking If this is the first time netering this room


                string? input = commandString.Trim().ToLower();


                //Checks if the command is null (nothing)
                if (string.IsNullOrEmpty(input))
                {
                    TypeLine("Please enter a command.");
                    continue;
                }
                //Checks if the command is null (nothing)

                //Checks whether the chosen option is a real command
                Command? command = parser.GetCommand(input);
                //Checks whether the chosen option is a real command

                //If command is null (can't be with the menu) then writes an 'error' message
                if (command == null)
                {
                    TypeLine("I don't know that command.");
                    continue;
                }
                //If command is null (can't be with the menu) then writes an 'error' message

                //Checks the option you chose, and makes the right call
                switch (command.Name)
                {
                    case "look":
                        TypeLine(currentRoom?.LongDescription);
                        Console.ReadKey();
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
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "quiz":
                        Quiz(CreateQuiz());
                        break;

                    case "read":
                        ReadBook();
                        break;
                    case "health":
                        Console.Clear();
                        TypeLine($"Scrappy's Health: {Scrappy.GetHealth()}");
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Press SPACE to go back");
                        Console.ReadKey();
                        break;

                    default:
                        TypeLine("I don't know what command.");
                        Console.ReadKey();
                        break;
                }
                //Checks the option you chose, and makes the right call

            }

            //Thanks the user for playing the game
            TypeLine("Thank you for playing THE WAY BACK HOME: A recycling adventure!!");
        }
        private static void ReadBook()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine('\t' + "_______________________________________________________________________________________________________");
            Console.WriteLine('\t' + "|                                                                                                     |");
            Console.WriteLine('\t' + "|     Within this living guide, unlock the subtle secrets of sustainability. Immerse yourself in      |");
            Console.WriteLine('\t' + "|     the mystical transformation of used materials into new treasures, an ancient process            |");
            Console.WriteLine('\t' + "|     preventing waste and nurturing a greener world. As you weave through the labyrinth of           |");
            Console.WriteLine('\t' + "|     knowledge, let your gaze linger on the symbols that reveal the widely embraced                  |");
            Console.WriteLine('\t' + "|     champion among recyclable materials. In this eco-realm, the three mysterious arrows             |");
            Console.WriteLine('\t' + "|     guide the way, representing the pillars of responsibility—Reduce, Reuse, and Recycle.           |");
            Console.WriteLine('\t' + "|     Venture further into the composting realms, where nature's alchemy transforms                   |");
            Console.WriteLine('\t' + "|     organic matter into a recycling ally. Amidst your exploration, discern the proper resting       |");
            Console.WriteLine('\t' + "|     place for old newspapers and magazines, and in doing so, you unlock the wisdom to               |");
            Console.WriteLine('\t' + "|     navigate the recycling hierarchy. This eco-adventure is your portal to enlightenment,           |");
            Console.WriteLine('\t' + "|     and these hidden cues shall be your allies on the path to a sustainable legacy.                 |");
            Console.WriteLine('\t' + "|                                                                                                     |");
            Console.WriteLine('\t' + "-------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press SPACE to return");
            Console.ReadKey();

        }
        private static void Quiz(MenuQuiz[] Questions)
        {
            foreach (var menu in Questions)
            {
                if (menu.Run() == 1)
                {
                    Scrappy.triviaPoints++;
                }
            }
            Console.WriteLine($"Score: {Scrappy.triviaPoints}" );
            Console.ReadKey();
        }
        private MenuQuiz[] CreateQuiz()
        {
            MenuQuiz[] questions = new MenuQuiz[]
            {
                new MenuQuiz("What is the process of turning used materials into new " +
                "products to prevent waste and reduce the consumption of fresh raw materials called?"
                , new string[] { "A) Repeating", "B) Recycling", "C) Replicating", "D) Refurbishing" }
                , "B) Recycling"),

                new MenuQuiz("Which of the following is a widely recycled material?"
                , new string[] { "A) Styrofoam", "B) Glass", "C) Plastic Bags", "D) All of the above" }
                , "D) All of the above"),

                new MenuQuiz("What do the three chasing arrows in the recycling symbol represent?"
                , new string[] { "A) Reduce, Reuse, Recycle", "B) Earth, Water, Air",
                    "C) Recyclable, Non-recyclable, Compostable",  "D) Collection, Processing, Remanufacturing"}
                , "A) Reduce, Reuse, Recycle"),

                new MenuQuiz("True or False: Composting is a form of recycling."
                , new string[] { "A) True", "B) False"}
                , "A) True"),

                new MenuQuiz("In which category of waste should old newspapers and magazines typically be placed for recycling?"
                , new string[] { "A) Paper", "B) Plastic", "C) Metal",  "D) Glass"}
                , "A) Paper"),

                new MenuQuiz("What is the term for the practice of collecting rainwater " +
                "for later use in activities like watering plants and flushing toilets?"
                , new string[] { "A) Rain Harvesting", "B) Water Conservation", "C) Rainwater Reuse", "D) Water Harvesting" }
                , "A) Rain Harvesting"),

                new MenuQuiz("Which of the following materials is NOT commonly accepted in curbside recycling programs:"
                , new string[] { "A) Aluminum cans", "B) Pizza boxes", "C) Plastic bottles", "D) Cardboard" }
                , "B) Pizza boxes"),

                new MenuQuiz("What does the acronym PET stand for in the context of recycling?"
                , new string[] { "A) Polyethylene Terephthalate", "B) Plastic Extraction Technology"
                , "C) Paper Elimination Technique", "D) Post-consumer Environmental Treatment" }
                , "A) Polyethylene Terephthalate"),

                new MenuQuiz("Which country is often cited as a leader in recycling" +
                ", with high rates of recycling and efficient waste management systems?"
                , new string[] { "A) United States", "B) Germany", "C) China", "D) Brazil" }
                , "B) Germany"),

                new MenuQuiz("True or False: Biodegradable and compostable plastics can be recycled in the same way as traditional plastics."
                , new string[] { "A) True", "B) False"}
                , "B) False"),

            };
            return questions;
        }
        private void Move(string direction) //Moves the player to the next room
        {
            
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                Room? room = currentRoom?.Exits[direction];
                if (room is FinalRoom)
                {
                    (room as FinalRoom).SetCanEnterTrue();
                    if ((room as FinalRoom).CanEnter() == true)
                    {
                        previousRoom = currentRoom;
                        currentRoom = currentRoom?.Exits[direction];
                    }
                    else
                    {
                        TypeLine("You don't have all the items to enter the Room!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    previousRoom = currentRoom;
                    currentRoom = currentRoom?.Exits[direction];
                }
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
            Console.WriteLine();
        } //Prints the welcome message at the launch of the game
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
        } //Prints the prologue for the game
        private static void TypeLinePrologue(string line)
        {
            Console.SetCursorPosition(0, 10);
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(1);
            }
        }  //Helps printing the prologue in a typeline way
        private static void TypeLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                System.Threading.Thread.Sleep(1);
            }
        } //Instead of Console.WriteLine() this can be used for a typeline effect
        private static void PrintHelp()
        {
            Console.Clear();
            Console.WriteLine(
                    "\r\n This is the game of The Way Back Home, A Recycling Adventure!" +
                    "\r\n In this game there are a few rooms, each of them will teach you" +
                    "\r\n about recycling. You can give commands to Scrappy by choosing " +
                    "\r\n options from a menu. Your health level is based on your chosen" +
                    "\r\n difficulty. Whenever you fail to do a task, you will lose a health" +
                    "\r\n point. If your health level drops to zero, the game is over." +
                    "\r\n" +
                    "\r\n Navigate by choosing 'NORTH', 'SOUTH', 'EAST', or 'WEST'" +
                    "\r\n LOOK for more details" +
                    "\r\n BACK to go to the previous room" +
                    "\r\n HELP to print this message again" +
                    "\r\n HEALTH to see Scrappy's health level" + 
                    "\r\n QUIT to exit the game" +
                    "\r\n" +
                    "\r\n also, in rooms there is usually a special and unique thing" +
                    "\r\n aswell. It could be a minigame you must do, or some information" +
                    "\r\n for you final challenge. ");
            Console.WriteLine();
            Console.WriteLine("Press SPACE to return");
            Console.ReadKey();
        } //Prints help, might delete later
    }
}



