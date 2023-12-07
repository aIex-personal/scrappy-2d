using ConsoleClient.GameElements;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security;
using System.Threading.Channels;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks.Sources;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleClient.SystemElements
{
    public class Game
    {
        //Ahmad
        //Initalizing Game's classes variables
        private Room? currentRoom;
        private Room? previousRoom;

        public static int difficulty = 0; //This variable will store the difficulty level
                                          //Initalized with a 0, Easy = 1, Medium = 2, Hard = 3

        private static bool canLeave;
        public static Player Scrappy;

        public Game() //constructor
        {
            Scrappy = new Player();
            canLeave = false;
            CreateRooms();
        }

        private void CreateRooms() //Creating the Rooms, setting their exits and setting the starting room
        {


            Room outside = new("Outside", @"Scrappy has landed his ship in front of a building, 
but his ship is broken.
He needs parts and fuel for his spaceship.
Without them, Scrappy will be stuck on planet Earth,
And his home planet will get destroyed from the waste for sure.
Scrappy needs to act fast.
There is a door to the North, the sign says Hall to the Recycling Centre.
There is also a wooden board next to the door, with some writing on it.",
@"Visiting outside, The ship still needs to be repaired, to the North there is the Hall",
new MenuPlain(
                    "\r\n Outside" +
                    "\r\n" +
                    "\r\n LOOK for more details in the room" +
                    "\r\n BACK to go to the previous room" +
                    "\r\n HELP to print guide for the game" +
                    "\r\n HEALTH to see Scrappy's health level" +
                    "\r\n ITEMS to look into your Inventory" +
                    "\r\n BOARD to read the wooden board's text" +
                    "\r\n LEAVE to leave the planet and travel home" +
                    "\r\n QUIT to exit the game" +
                    "\r\n ", new string[] { "NORTH ", "LOOK  ", "BACK  ","HEALTH","ITEMS ",  "HELP  ", "READ  ","LEAVE ", "QUIT  " }
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
                    "\r\n HELP to print guide for the game" +
                    "\r\n HEALTH to see Scrappy's health level" +
                    "\r\n ITEMS to look into your Inventory" +
                    "\r\n QUIT to exit the game" +
                    "\r\n ", new string[] {"NORTH ", "EAST  ", "SOUTH ", "WEST  ",
                    "LOOK  ", "BACK  ","HEALTH","ITEMS ","HELP  ","ROBOT ", "QUIT  "}));

            Room compost = new("Composting Garden", @"Scrappy has entered a garden, but it was different
from the gardens that he has seen in his life before. 
On His Planet, Scrappy and his people used to 
Throw the organic leftovers out to the bin.
But here, people on Earth are using them to
grow more plants. How is this possible?
In the garden there is also a snake, 
Maybe I should help it collect the junk from the garden.
The only door is back to East, the Hall.",
                                          @"
You are in the Composting garden, the only door
leads back to the hall. In the garden there is 
a Snake.",
                                          new MenuPlain(
                    "\r\n Composting Garden" +
                    "\r\n" +
                    "\r\n LOOK for more details" +
                    "\r\n BACK to go to the previous room" +
                    "\r\n HELP to print guide for the game" +
                    "\r\n HEALTH to see Scrappy's health level" +
                    "\r\n ITEMS to look into your Inventory" +
                    "\r\n QUIT to exit the game" +
                    "\r\n SNAKE to help it collect the garbage" +
                    "\r\n ", new string[] { "EAST  ", "LOOK  ", "BACK  ", "HELP  ","HEALTH","ITEMS ", "SNAKE ", "QUIT  " }));

            Room recyclingRoom = new("Recycling Room", @"
Scrappy enters the next room. Here people are
sorting the garbages out. In Scrappy's planet, people 
would simply throw everything out to the trash bin,
but here they are throwing different things out into
different bins. How strange. They told Scrappy that
he could help if he wanted to.
The only door to the West leads back to the Hall",
                                          @"
You have entered the Recycling Room. 
People are sorting things out rather than
Just throwing them into a bin.
To the West, there is the door back to the Hall",
                                          new MenuPlain(
                    "\r\n Recycling Room" +
                    "\r\n" +
                    "\r\n LOOK for more details" +
                    "\r\n BACK to go to the previous room" +
                    "\r\n HELP to print this message again" +
                    "\r\n HEALTH to see Scrappy's health level" +
                    "\r\n ITEMS to look into your Inventory" +
                    "\r\n SORT to help sorting the garbage"+
                    "\r\n QUIT to exit the game" +
                    "\r\n ", new string[] { "WEST  ", "LOOK  ", "BACK  ","HEALTH","ITEMS ", "HELP  ", "SORT  ", "QUIT  " }));
            FinalRoom mysteryRoom = new FinalRoom("Final mission Room", 
                @"
As Scrappy Enter the Mysterious Room, he can see one thing only.
A Quiz, a test that he needs to do. The Knowledge, that he has
received while being on Earth, is going to be put on a test.
If he is able to do it successfully, there may be a small chance to
save his planet. But if not, it could be destroyed for all eternity.",
                                                "Final Mission is here. Take the quiz to be able to save your planet",
                                               new MenuPlain(
                    "\r\n Final Room" +
                    "\r\n" +
                    "\r\n LOOK for more details" +
                    "\r\n BACK to go to the previous room" +
                    "\r\n HELP to print guide for the game" +
                    "\r\n HEALTH to see Scrappy's health level" +
                    "\r\n ITEMS to look into your Inventory" +
                    "\r\n QUIZ for playing the final quiz" +
                    "\r\n QUIT to exit the game" +
                    "\r\n ", new string[] {"SOUTH ", "LOOK  ", "BACK  ", "HEALTH", "ITEMS ","HELP  ","QUIZ  ", "QUIT  "}));

            
            outside.SetExit("north", hall);
            hall.SetExits(mysteryRoom, recyclingRoom, outside, compost);
            recyclingRoom.SetExit("west", hall);
            compost.SetExit("east", hall);
            mysteryRoom.SetExit("south", hall);
            currentRoom = outside;
        }   
        //github change
        public void Play() //Playing the Game
        {
            Parser parser = new();
            PrintWelcome();
            PrintPrologue();

            string commandString;
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
                    //Console.WriteLine(currentRoom.FirstDescription);
                    Console.ReadKey();
                    currentRoom.SetFirstEnterFalse();
                    commandString = currentRoom.commandMenu.Run();
                }
                else
                {
                    //If no, just write the default description of the room. 
                    commandString = currentRoom.commandMenu.Run();
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
                        Quiz();
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

                    case "robot":
                        RobotConversation();
                        break;

                    case "snake":
                        SnakeMinigame();
                        break;

                    case "items":
                        PrintInventory();
                        break;

                    case "sort":
                        SortingMingame();
                        break;

                    case "leave":
                        if (canLeave)
                        {
                            EndGameSuccess();
                        }
                        else
                        {
                            TypeLine(@"You don't have all the items to repair your broken ship,
so you cannot leave the planet yet.");
                            Console.ReadKey();
                        }
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
        private static void EndGameSuccess()
        {
            Console.Clear(); 
            TypeLinePrologue(@"Congratulations!
You have finished the game! 
You did really well, 
And helped Scrappy go home
in order to save his planet.
Thank you for playing!");
            Console.ReadKey();
            Environment.Exit(0);
        }
        private static void EndGameFailure()
        {
            TypeLinePrologue(@"Sorry, but Scrappy has
lost all of his health.
You will have to start
over in order to help
him succeed.
");
            Console.ReadKey();
            Environment.Exit(0);
        }
        private static void IsHealthZero()
        {
            if (Scrappy.GetHealth() == 0)
            {
                EndGameFailure();
            }
        }
        private static void SortingMingame()
        {
            MenuQuiz[] questions = new MenuQuiz[]
            {
                new MenuQuiz("How can you prepare plastic containers for recycling?",
                new string[]{ "A) Leave the lids on", "B) Rinse them clean", "C) Paint them for decoration"},
                "B) Rinse them clean"),
                new MenuQuiz("Which type of paper product is commonly accepted in recycling bins?",
                new string[]{ "A) Soiled pizza boxes", "B) Shredded paper", "C) Cardboard boxes"},
                "C) Cardboard boxes"),
                new MenuQuiz("What's a good practice before recycling aluminum cans?",
                new string[]{ "A) Flatten them", "B) Leave them uncrushed", "C) Use them to store other items"},
                "A) Flatten them"),
                new MenuQuiz("How should you handle broken glass items for recycling?",
                new string[]{ "A) Place them directly in the recycling bin",
                    "B) Wrap them in newspaper and dispose of them in the trash",
                    "C) Take them to a glass recycling center"},
                "B) Wrap them in newspaper and dispose of them in the trash"),
                new MenuQuiz("What should you do with old electronic devices like computers and monitors?",
                new string[]{ "A) Throw them in the regular trash",
                    "B) Recycle them at a specialized facility",
                    "C) Donate them to a local charity"},
                "B) Recycle them at a specialized facility")

            };
            foreach (var menu in questions)
            {
                if (menu.Run() == 1)
                {
                    Scrappy.triviaPoints++;
                }
            }
            if (Scrappy.triviaPoints >= 3)
            {
                Console.Clear();
                TypeLine($"Score: {Scrappy.triviaPoints}");
                Console.WriteLine();
                Console.WriteLine();
                TypeLine(@"You have successfully made this quiz!
You are one step closer to get 
All the Knowledge that you need
To save Scrappy's planet!");
                canLeave = true;
                if (!Scrappy.inventory.Contains("SpaceShip Parts"))
                {
                    Scrappy.inventory.Add("SpaceShip Parts");
                }
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                TypeLine($"Score: {Scrappy.triviaPoints}");
                Console.WriteLine();
                Console.WriteLine();
                TypeLine("You have failed the quiz. Try again!");
                Scrappy.LoseHealth();
                Console.ReadKey();
                IsHealthZero();
            }
            Scrappy.triviaPoints = 0;
        }
        private static void SnakeMinigame()
        {
            
            MenuQuiz[] questions = new MenuQuiz[]
            {
                new MenuQuiz("What is the primary purpose of composting in a garden?",
                new string[]{ "A) Fertilizing plants", "B) Creating a cozy atmosphere", "C) Attracting butterflies"},
                "A) Fertilizing plants"),
                new MenuQuiz("Which of the following is a green (nitrogen-rich) material suitable for composting?",
                new string[]{ "A) Dry leaves", "B) Coffee grounds", "C) Twigs"},
                "B) Coffee grounds"),
                new MenuQuiz("Why is it important to regularly turn or mix compost?",
                new string[]{ "A) To keep it warm", "B) To aerate and speed up decomposition", "C) To attract worms"},
                "B) To aerate and speed up decomposition"),
                new MenuQuiz("If your compost pile smells bad, what might be the issue?",
                new string[]{ "A) It needs more water", "B) It has too much brown material", "C) It's too small"},
                "A) It needs more water"),
                new MenuQuiz("Besides enriching the soil, what is another benefit of composting?",
                new string[]{ "A) Repelling pests", "B) Reducing landfill waste", "C) Attracting birds"},
                "B) Reducing landfill waste")

            };
            foreach (var menu in questions)
            {
                if (menu.Run() == 1)
                {
                    Scrappy.triviaPoints++;
                }
            }
            if (Scrappy.triviaPoints >= 3)
            {
                Console.Clear();
                TypeLine($"Score: {Scrappy.triviaPoints}");
                Console.WriteLine();
                Console.WriteLine();
                TypeLine(@"You have successfully made this quiz!
You are one step closer to get 
All the Knowledge that you need
To save Scrappy's planet!");
                canLeave = true;
                if (!Scrappy.inventory.Contains("Biofuel"))
                {
                    Scrappy.inventory.Add("Biofuel");
                }
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                TypeLine($"Score: {Scrappy.triviaPoints}");
                Console.WriteLine();
                Console.WriteLine();
                TypeLine("You have failed the quiz. Try again!");
                Scrappy.LoseHealth();
                Console.ReadKey();
                IsHealthZero();
            }
            Scrappy.triviaPoints = 0;
        }
        private static void RobotConversation()
        {
            string text = @"
Greetings, Traveler!
Step into the Sustainability Nexus, where the journey unfolds.
Imagine a world where rainwater becomes a resource—a practice known as Rain Harvesting,
where water conservation dances with nature's abundance. Now, in the recycling cosmos,
navigate materials like aluminum cans and plastic bottles joining the curbside recycling ballet,
while the rebellious pizza boxes choose a different stage. Decode the enigma of PET,
where the code conceals its true form Polyethylene Terephthalate, a key player in the recycling saga.
Venture globally, and discover the recycling utopia, where Germany reigns, a leader with high rates
and an efficient waste management symphony. Lastly, in the plastic paradox,
discern the truth False as biodegradable and compostable plastics take a different route.
Your quest for environmental enlightenment has just begun. ";
            Console.Clear();
            TypeLine(text);
            Console.ReadKey();
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
        private static void Quiz()
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
            foreach (var menu in questions)
            {
                if (menu.Run() == 1)
                {
                    Scrappy.triviaPoints++;
                }
            }
            if (Scrappy.triviaPoints > 5)
            {
                Console.Clear();
                TypeLine($"Score: {Scrappy.triviaPoints}");
                Console.WriteLine(); 
                Console.WriteLine();
                TypeLine(@"You have Won! You can leave the planet!
Go back to your ship and with your new parts
repair your ship, then travel home
and Save Your Planet!");
                canLeave = true;
                if (!Scrappy.inventory.Contains("Knowledge"))
                {
                    Scrappy.inventory.Add("Knowledge");
                }
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                TypeLine($"Score: {Scrappy.triviaPoints}");
                Console.WriteLine();
                Console.WriteLine();
                TypeLine("You have failed the quiz. Try again!");
                Scrappy.LoseHealth();
                Console.ReadKey();
                IsHealthZero();
            }
            Scrappy.triviaPoints = 0;
        }
        
        private void Move(string direction) //Moves the player to the next room
        {
            
            if (currentRoom?.Exits.ContainsKey(direction) == true)
            {
                Room? room = currentRoom?.Exits[direction];
                if (room is FinalRoom)
                {
                    if (Scrappy.inventory.ReadAll().Count == 5)
                    {
                        (room as FinalRoom).SetCanEnterTrue();
                        Console.Clear();
                        TypeLine("The mysterious room has opened!");
                        Console.ReadKey();
                    }
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
        private static void PrintInventory()
        {
            Console.Clear();
            var items = Scrappy.inventory.ReadAll();
            foreach (var item in items)
            {
                TypeLine(item);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}



