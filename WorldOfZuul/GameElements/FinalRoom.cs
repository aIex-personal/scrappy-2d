﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.GameElements
{
    internal class FinalRoom : Room
    {
        public FinalRoom(string shortDesc, string longDesc, string firstDescription) : base(shortDesc, longDesc, firstDescription)
        {
            canEnter = false;
        }
        private bool canEnter;
        public bool CanEnter()
        {
            return canEnter;
        }
        public void SetCanEnterTrue()
        {
            canEnter = true;
        }
    }
}