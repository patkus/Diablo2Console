using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console
{
    public class Smith
    {
        public const string Name = "Charsie";

        public readonly Dictionary<string, string> SpokenLines = new Dictionary<string, string>()
        {
            { "Chat",
            @"Hi there. I'm Charsi, the Blacksmith here in camp. It's good to see some strong adventurers around here.

            Many of our Sisters fought bravely against Diablo when he first attacked the town of Tristram.

            They came back to us true veterans, bearing some really powerful items.Seems like their victory was short-lived, though...Most of them are now corrupted by Andariel."
            },
            {"Repair", "Your stuff got shinier! :3"}
        };

        public Smith(ActionMenuService actionMenuService)
        {
            actionMenuService.AddNewActionMenu(ConsoleKey.C, "Chat", "Smith");
            actionMenuService.AddNewActionMenu(ConsoleKey.C, "Repair", "Smith");
        }
    }
}
