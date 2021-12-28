using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class OffensiveItem : Item
    {
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public OffensiveItem() { }
        public OffensiveItem(string name, int durability, int minDamage, int maxDamage, string itemType)
        {
            Name = name;
            Durability = durability;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            ItemType = itemType;
        }
        public override void ShowItem()
        {
            base.ShowItem();
            Console.WriteLine($"Damage: {MinDamage}-{MaxDamage}");
        }
    }
}
