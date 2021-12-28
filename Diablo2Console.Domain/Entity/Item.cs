using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public int Durability { get; set; }
        public string ItemType { get; set; }

        public virtual void ShowItem()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Durability: {Durability}");
        }
    }
}
