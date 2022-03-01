using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diablo2Console.App.Concrete
{
    public class TaskFunctionService : BaseService<TaskFunction>
    {
        private TaskService _taskService;

        public TaskFunctionService(TaskService taskService)
        {
            _taskService = taskService;

            Initialize();
        }

        private void Initialize()
        {
            Task task = _taskService.GetAllItems().Where(x => x.Name == "Quest1Task1").FirstOrDefault();
            if (task != null)
            { 
                CreateItem(new TaskFunction(GetNextId(), task.Id, "SpeakToAkara"));
            }
        }

        public void SpeakToAkara()
        {
            var task = _taskService.GetAllItems().Where(x => x.Name == "Quest1Task1").FirstOrDefault();
            task.Finished = true;
            task.Active = false;
            _taskService.UpdateItem(task);
            _taskService.ActivateNextTaskOrFinishQuest(task);
        }
    }
}
