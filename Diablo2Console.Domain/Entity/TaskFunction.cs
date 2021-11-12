using Diablo2Console.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.Domain.Entity
{
    public class TaskFunction : BaseEntity
    {
        public int TaskId { get; set; }
        public string Name { get; set; }

        public TaskFunction(int id, int taskId, string name)
        {
            Id = id;
            TaskId = taskId;
            Name = name;
        }
    }
}
