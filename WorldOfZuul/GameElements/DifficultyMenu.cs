﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.GameElements
{
    public class DifficultyMenu : Menu
    {
        private string[] Difficulties;
        public DifficultyMenu(string Prompt, string[] Options, string[] Difficulties) : base(Prompt, Options)
        {
            this.Prompt = Prompt;
            this.Options = Options;
            this.Difficulties = Difficulties;
            middleX = (Console.WindowWidth - 24) / 2;
            middleY = Console.WindowHeight / 2;
            SelectedIndex = 0;
        }

        protected override void DisplayOptions()
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
                Write($"|     {prefix} {currentOption} {suffix}     |");
                Console.ResetColor();
                Write(currentDifficulty);
                Write("|                      |");
            }
            Console.ResetColor();
            Write("|======================| ");
            middleY = Console.WindowHeight / 2;
        }
        public override int Run()
        {
            return base.Run();
        }
    }
}
