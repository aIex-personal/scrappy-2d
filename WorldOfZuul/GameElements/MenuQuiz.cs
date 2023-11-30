using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.GameElements
{
    public class MenuQuiz
    {
        protected int SelectedIndex;
        protected string[] Options;
        protected string Prompt;
        protected string RightAnswer;

        public MenuQuiz(string Prompt, string[] Options, string RightAnswer)
        {
            this.Prompt = Prompt;
            this.Options = Options;
            SelectedIndex = 0;
            this.RightAnswer = RightAnswer;
        }

        protected virtual void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine(Prompt);
            Console.WriteLine(" ");
            Console.WriteLine("|======================|");
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
                Console.WriteLine($" {prefix} {currentOption} {suffix} ");
                Console.ResetColor();
            }
            Console.ResetColor();
            Console.WriteLine("|======================|");
            Console.WriteLine();
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
                    if (SelectedIndex == -1)
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
            if (Options[SelectedIndex] == RightAnswer)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
