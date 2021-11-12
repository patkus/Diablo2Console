using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public int QuestId { get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public bool Finished { get; set; }

        public Task(int id, string name, string message, int questId, int order, bool active = false, bool finished = false)
        {
            Id = id;
            Name = name;
            Message = message;
            QuestId = questId;
            Order = order;
            Active = active;
            Finished = finished;
        }
    }
}
