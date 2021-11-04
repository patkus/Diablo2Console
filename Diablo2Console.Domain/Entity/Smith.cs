using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class Smith : Npc
    {
        public Smith(string name)
        {
            base.Name = name;

            if(name == "Charsie")
            {
                base.SpokenLines.Add("Chat", @"Hi there. I'm Charsi, the Blacksmith here in camp. It's good to see some strong adventurers around here.

Many of our Sisters fought bravely against Diablo when he first attacked the town of Tristram.

They came back to us true veterans, bearing some really powerful items.Seems like their victory was short-lived, though...Most of them are now corrupted by Andariel.");

                base.SpokenLines.Add("Repair", "Items repaired!");
                base.SpokenLines.Add("GoodBye", "See you later!");
            }
        }
    }
}
