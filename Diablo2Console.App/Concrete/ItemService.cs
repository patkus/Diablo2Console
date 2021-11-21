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
            Items.Add(new DefensiveItem("Basic Armor", 30, 5));
            Items.Add(new DefensiveItem("Basic Gloves", 10, 5));
            Items.Add(new DefensiveItem("Basic Boots", 10, 5));
            Items.Add(new DefensiveItem("Basic Helmet", 20, 5));
            Items.Add(new DefensiveItem("Basic Shield", 20, 5));
            Items.Add(new OffensiveItem("Basic Sword", 10, 1, 3));
        }
    }
}
