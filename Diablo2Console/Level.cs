using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console
{
    public class Level
    {
        public char[,] Map { get; }

        public Level(char[,] map)
        {
            Map = map;
        }
    }
}
