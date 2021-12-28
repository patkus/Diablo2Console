using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class DefensiveItem : Item
    {
        public int Armor { get; set; }
        public DefensiveItem() { }
        public DefensiveItem(string name, int durability, int armor, string itemType)
        {
            Name = name;
            Durability = durability;
            Armor = armor;
            ItemType = itemType;
        }
        public override void ShowItem()
        {
            base.ShowItem();
            Console.WriteLine($"Armor: {Armor}");
        }
    }
}
