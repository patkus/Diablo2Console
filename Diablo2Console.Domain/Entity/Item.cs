using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Diablo2Console.Domain.Entity
{
    public class Item : BaseEntity
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Durability")]
        public int Durability { get; set; }
        [XmlElement("ItemType")]
        public string ItemType { get; set; }

        public Item() { }
        public virtual void ShowItem()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Durability: {Durability}");
        }
    }
}
