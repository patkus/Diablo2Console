using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Diablo2Console.App.Concrete
{
    public class PlayerService : BaseService<Player>
    {
        public static int PlayerBasicHealth = 50;
        private ItemService _itemService { get; set; }
        public PlayerService(ItemService itemService)
        {
            _itemService = itemService;
        }
        public override int CreateItem(Player newPlayer)
        {
            var newPlayerId = base.CreateItem(newPlayer);

            Initialize(newPlayer);

            return newPlayerId;
        }

        public int LoadItem(Player newPlayer)
        {
            var newPlayerId = base.CreateItem(newPlayer);

            Load(newPlayer);

            return newPlayerId;
        }

        private void Initialize(Player newPlayer)
        {
            var startingItems = _itemService.GetAllItems().Where(x => x.Name == "Basic Sword" || x.Name == "Basic Shield");

            foreach (var startingItem in startingItems)
            {
                newPlayer.PlayerBag.Add(startingItem);
            }
        }

        private void Load(Player player)
        {
            var filePath = @"../../../files/Save/PlayerBagDefensiveItem.xml";
            if (File.Exists(filePath))
            {
                foreach (var defensiveItem in Helpers.Helper.ReadFromXml<DefensiveItem>(filePath))
                {
                    player.PlayerBag.Add(defensiveItem);
                }
            }
            filePath = @"../../../files/Save/PlayerBagOffensiveItem.xml";
            if (File.Exists(filePath))
            {
                foreach (var offensiveItem in Helpers.Helper.ReadFromXml<OffensiveItem>(filePath))
                {
                    player.PlayerBag.Add(offensiveItem);
                }
            }
            filePath = @"../../../files/Save/PlayerMap.txt";
            if (File.Exists(filePath))
            {
                List<char[]> levelAsList = new List<char[]>();
                string[] fileInLines = File.ReadAllLines(filePath);

                foreach (var textLine in fileInLines)
                {
                    levelAsList.Add(textLine.ToCharArray());
                }

                player.PlayerMap = Helpers.Helper.JaggedIntoMultidimensionalArray(levelAsList.ToArray());
            }
        }
    }
}
