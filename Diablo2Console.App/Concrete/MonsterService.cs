using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.App.Concrete
{
    public class MonsterService : BaseService<Monster>
    {
        public MonsterService()
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateItem(new Monster("Fallen", 20, 1, 3, 'f', new List<string>() { "Level1" }));
            CreateItem(new Monster("Fallen Lord", 30, 2, 4, 'F', new List<string>() { "Level1" }));
        }
    }
}
