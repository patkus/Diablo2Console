using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class ActionMenu : BaseEntity
    {
        public ConsoleKey InputKey { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }

        public ActionMenu(ConsoleKey inputKey, string name, string group)
        {
            InputKey = inputKey;
            Name = name;
            Group = group;
        }
    }
}
