using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class Monster : BaseEntity
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public char MapSymbol { get; set; }
        public List<string> LevelIdsAppearance { get; set; }

        public Monster(string name, int health, int minDamage, int maxDamage, char mapSymbol, List<string> levelIdsAppearance)
        {
            Name = name;
            Health = health;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            MapSymbol = mapSymbol;
            LevelIdsAppearance = levelIdsAppearance;
        }
    }
}
