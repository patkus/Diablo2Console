﻿using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Diablo2Console.Domain.Entity
{
    public class OffensiveItem : Item
    {
        [XmlElement("MinDamage")]
        public int MinDamage { get; set; }
        [XmlElement("MaxDamage")]
        public int MaxDamage { get; set; }
        public OffensiveItem() { }
        public OffensiveItem(int id, string name, int durability, int minDamage, int maxDamage, string itemType)
        {
            Id = id;
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
