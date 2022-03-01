using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diablo2Console.App.Concrete
{
    public class TaskService : BaseService<Task>
    {
        private QuestService _questService;

        public TaskService(QuestService questService)
        {
            _questService = questService;

            Initialize();
        }

        public void Initialize()
        {
            //First quest tasks
            Quest quest = _questService.GetAllItems().Where(x => x.Name == "The Den of Evil").FirstOrDefault();
            if (quest != null)
            {
                CreateItem(new Task(GetNextId(), "Quest1Task1", "Speak to Akara.", quest.Id, 1, true));
            }
        }

        public void ActivateNextTaskOrFinishQuest(Task finishedTask)
        {
            var nextTask = GetAllItems().Where(x => x.Name == finishedTask.Name && x.Order == ++finishedTask.Order).FirstOrDefault();

            if(nextTask != null)
            {
                nextTask.Active = true;
                UpdateItem(nextTask);
            }
            else
            {
                var currentQuest = _questService.GetAllItems().Where(x => x.Id == finishedTask.QuestId).FirstOrDefault();

                if(currentQuest != null)
                {
                    currentQuest.Finished = true;
                    currentQuest.Active = false;
                    _questService.UpdateItem(currentQuest);
                }
            }
        }
    }
}
