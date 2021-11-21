using Diablo2Console.Domain.Common;
using System.Collections.Generic;

namespace Diablo2Console.Domain.Entity
{
    public class Player : BaseEntity
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char[,] PlayerMap { get; set; }
        public List<Item> PlayerBag { get; set; }

        public Player()
        {
            PlayerBag = new List<Item>();
        }
    }
}
