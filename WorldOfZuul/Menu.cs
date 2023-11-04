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
        protected int SelectedIndex;
        protected string[] Options;
        protected string Prompt;


        protected int middleX;
        protected int middleY;

        public Menu(string Prompt, string[] Options)
        {
            this.Prompt = Prompt;
            this.Options = Options;
            SelectedIndex = 0;
            this.middleX = (Console.WindowWidth - 24) / 2 + 5;
            this.middleY = Console.WindowHeight / 2 - 10;
        }

        protected void Write(string s)
        {
            Console.SetCursorPosition(middleX, middleY);
            Console.WriteLine(s);
            middleY++;
        }


        protected virtual void DisplayOptions()
        {
            Console.Clear();
            Write(Prompt);
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

                Write($"{prefix} {currentOption} {suffix}");
            }
            Console.ResetColor();
            this.middleY = Console.WindowHeight / 2 - 10;
        }
        public virtual int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                DisplayOptions();

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


            } while (keyPressed != ConsoleKey.Enter && keyPressed != ConsoleKey.Spacebar);

            return SelectedIndex;
        }
    }
}
