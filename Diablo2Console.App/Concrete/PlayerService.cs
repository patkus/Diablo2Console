using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
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
        private void Initialize(Player newPlayer)
        {
            var startingItems = _itemService.GetAllItems().Where(x => x.Name == "Basic Sword" || x.Name == "Basic Shield");

            foreach (var startingItem in startingItems)
            {
                newPlayer.PlayerBag.Add(startingItem);
            }
        }
    }
}
