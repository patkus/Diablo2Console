using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Diablo2Console.Domain.Entity
{
    public class DefensiveItem : Item
    {
        [XmlElement("Armor")]
        public int Armor { get; set; }
        public DefensiveItem() { }
        public DefensiveItem(int id, string name, int durability, int armor, string itemType)
        {
            Id = id;
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
