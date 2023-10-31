using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfZuul
{
    public class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;
        private string[] Difficulties;
        private int middleX;
        private int middleY;

        public Menu(string Prompt, string[] Options)
        {
            this.Prompt = Prompt;
            this.Options = Options;
            SelectedIndex = 0;
        }
        public Menu(string Prompt, string[] Options, string[] Difficulties)
        {
            this.Prompt = Prompt;
            this.Options = Options;
            this.Difficulties = Difficulties;
            this.middleX = (Console.WindowWidth - 24) / 2;
            this.middleY = Console.WindowHeight / 2 - 10;
            SelectedIndex = 0;
        }
        
        private void Write(string s)
        {
            Console.SetCursorPosition(middleX, middleY);
            Console.WriteLine(s);
            middleY++;
        }
        private void DisplayDifficulties()
        {
            Write(Prompt);
            Write(" ");
            Write("|======================|");
            Write("|                      |");
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string currentDifficulty = Difficulties[i];
                string prefix;
                string suffix;
                if (i == SelectedIndex)
                {
                    prefix = "<<";
                    suffix = ">>";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;

                }
                else
                {
                    prefix = "  ";
                    suffix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                //Console.WriteLine($"|     {prefix} {currentOption} {suffix}     |");
                //Console.ResetColor();
                //Console.WriteLine($"{currentDifficulty}");
                //Console.WriteLine("|                      |");

                Write($"|     {prefix} {currentOption} {suffix}     |");
                Console.ResetColor();
                Write(currentDifficulty);
                Write("|                      |");
            }
            //Console.ResetColor();
            //Console.WriteLine("|======================| ");
            Console.ResetColor();
            Write("|======================| ");
            this.middleY = Console.WindowHeight / 2 - 10;
        }
        private void DisplayOptions()
        {
            Console.WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;
                string suffix;
                if (i == SelectedIndex)
                {
                    prefix = "<<";
                    suffix = ">>";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    suffix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{prefix} {currentOption} {suffix}");
            }
            Console.ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                //DisplayOptions();
                DisplayDifficulties();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if(SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }


            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
