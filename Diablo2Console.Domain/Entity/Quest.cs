using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class Quest : BaseEntity
    {
        public string Name { get; set; }
        public int LevelId{ get; set; }
        public int Order { get; set; }
        public bool Finished { get; set; }
        public bool Active { get; set; }

        public Quest(int id, string name, int levelId, int order, bool active = false, bool finished = false)
        {
            Id = id;
            Name = name;
            LevelId = levelId;
            Order = order;
            Active = active;
            Finished = finished;
        }
    }
}
