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
        public int Health { get; set; }
        public int SummedMinDamage { get; set; }
        public int SummedMaxDamage { get; set; }
        public int SummedArmor { get; set; }

        public Player(int id, int health)
        {
            Id = id;
            Health = health;
            PlayerBag = new List<Item>();
        }
    }
}
