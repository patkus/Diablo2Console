using Diablo2Console.Domain.Common;
using System.Collections.Generic;

namespace Diablo2Console.Domain.Entity
{
    public class Level : BaseEntity
    {
        public string Name { get; set; }
        public char[,] Map { get; set; }
        public bool CurrentlyPlaying { get; set; }
        public int PlayerStartingPositionX { get; set; }
        public int PlayerStartingPositionY { get; set; }
        public Level(int id, string name, char[,] map, bool currentlyPlaying = true)
        {
            Name = name;
            Map = map;
            Id = id;
            CurrentlyPlaying = currentlyPlaying;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i, j] == 'P')
                    {
                        PlayerStartingPositionX = i;
                        PlayerStartingPositionY = j;
                    }
                }
            }
        }
    }
}
