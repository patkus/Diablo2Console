using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Diablo2Console.App.Concrete
{
    public class ItemService : BaseService<Item>
    {
        public ItemService()
        {
            Initialize();
        }

        private void Initialize()
        {
            var hostPath = $"../../../files/Item";
            var filePath = $"{hostPath}/DefensiveItem.json";
            foreach (var defensiveItem in JsonConvert.DeserializeObject<List<DefensiveItem>>(Helpers.Helper.ReadFromJson(filePath)))
            {
                defensiveItem.Id = GetNextId();
                CreateItem(defensiveItem);
            }
            filePath = $"{hostPath}/OffensiveItem.json";
            foreach (var offensiveItem in JsonConvert.DeserializeObject<List<OffensiveItem>>(Helpers.Helper.ReadFromJson(filePath)))
            {
                offensiveItem.Id = GetNextId();
                CreateItem(offensiveItem);
            }
        }
    }
}
