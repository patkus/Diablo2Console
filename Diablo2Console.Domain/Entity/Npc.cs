using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class Npc : BaseEntity
    {
        public string Name { get; set; }
        public Dictionary<string, string> SpokenLines = new Dictionary<string, string>();
    }
}
