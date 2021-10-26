using Diablo2Console.Domain.Common;

namespace Diablo2Console.Domain.Entity
{
    public class Level : BaseEntity
    {
        public string Name { get; set; }
        public char[,] Map { get; set; }
        public bool CurrentlyPlaying { get; set; }
        public Level(int id, string name, char[,] map, bool currentlyPlaying = true)
        {
            Name = name;
            Map = map;
            Id = id;
            CurrentlyPlaying = currentlyPlaying;
        }
    }
}
