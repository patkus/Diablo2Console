using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

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
            Items.Add(new DefensiveItem(GetNextId(), "Basic Armor", 30, 5, "DefensiveItem"));
            Items.Add(new DefensiveItem(GetNextId(), "Basic Gloves", 10, 5, "DefensiveItem"));
            Items.Add(new DefensiveItem(GetNextId(), "Basic Boots", 10, 5, "DefensiveItem"));
            Items.Add(new DefensiveItem(GetNextId(), "Basic Helmet", 20, 5, "DefensiveItem"));
            Items.Add(new DefensiveItem(GetNextId(), "Basic Shield", 20, 5, "DefensiveItem"));
            Items.Add(new OffensiveItem(GetNextId(), "Basic Sword", 10, 1, 3, "OffensiveItem"));
        }
    }
}
