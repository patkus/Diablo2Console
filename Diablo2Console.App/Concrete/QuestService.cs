using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Diablo2Console.App.Managers;

namespace Diablo2Console.App.Concrete
{
    public class QuestService : BaseService<Quest>
    {
        private LevelService _levelService;
        public QuestService(LevelService levelService)
        {
            _levelService = levelService;
            Initialize();
        }
        private void Initialize()
        {
            //First Act quests
            int levelId = _levelService.GetAllItems().Where(x => x.Name == "Level1").FirstOrDefault().Id;
            int questId = CreateItem(new Quest(GetNextId(), "The Den of Evil", levelId, 1, true));
        }

        public void ActivateNextQuest(Quest finishedQuest)
        {
            var nextQuest = GetAllItems().Where(x => x.Order == ++finishedQuest.Order).FirstOrDefault();

            if (nextQuest != null)
            {
                nextQuest.Active = true;
                UpdateItem(nextQuest);
            }
        }
    }
}
