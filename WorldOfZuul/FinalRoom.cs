﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldOfZuul;

namespace ConsoleClient
{
    internal class FinalRoom : Room
    {
        public FinalRoom(string shortDesc, string longDesc, string firstDescription) : base(shortDesc, longDesc, firstDescription)
        {
            canEnter = false;
        }
        private bool canEnter;
        public void SetCanEnterTrue()
        {
            canEnter = true;
        }
    }
}