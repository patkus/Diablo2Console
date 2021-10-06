﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console
{
    public class Player
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Player(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }
    }
}
