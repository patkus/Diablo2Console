using System;
using Xunit;
using Moq;
using Diablo2Console.Domain.Entity;
using Diablo2Console.App.Abstract;
using Diablo2Console.App.Managers;
using Diablo2Console.App.Concrete;
using System.Linq;
using System.Collections.Generic;

namespace Diablo2Console.Tests
{
    public class BaseServiceUnitTest
    {
        [Fact]
        public void GetItemByIdReturnsProvidedItemsId()
        {
            //Arrange
            Player player = new Player(1, 500);
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.GetItemById(1)).Returns(player);

            var playerManager = new PlayerManager(mock.Object, new LevelService(), new ActionMenuService(), new MonsterService());
            //Act
            var returnedPlayer = playerManager.GetPlayerById(player.Id);
            //Assert
            Assert.Equal(player, returnedPlayer);
        }
        [Fact]
        public void GetNextIdReturnsNextItemsId()
        {
            //Arrange
            Player player = new Player(1, 500);
            Player nextPlayer = new Player(2, 500);
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.GetNextId()).Returns(2);

            var playerManager = new PlayerManager(mock.Object, new LevelService(), new ActionMenuService(), new MonsterService());
            //Act
            int nextPlayerId = playerManager.GetNextPlayerId();
            //Assert
            Assert.Equal(nextPlayerId, nextPlayer.Id);
        }
        [Fact]
        public void CreateItemReturnsNewlyCreatedItemsId()
        {
            //Arrange
            Player player = new Player(1, 500);
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.CreateItem(player)).Returns(1);

            var playerService = mock.Object;
            //Act
            var returnedPlayer = playerService.CreateItem(player);
            //Assert
            Assert.Equal(player.Id, returnedPlayer);
        }

        [Fact]
        public void RemoveItemReturnsNullIfItemNotExistsInCollection()
        {
            //Arrange
            Player player = new Player(1, 500);
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.GetAllItems()).Returns(new List<Player>());

            var playerService = mock.Object;
            //Act
            playerService.CreateItem(player);
            playerService.RemoveItem(player);
            //Assert
            Assert.Null(playerService.GetAllItems().FirstOrDefault(x => x.Id == 1));
        }

        [Fact]
        public void GetAllItemsCollectionContainsAllItems()
        {
            //Arrange
            Player player = new Player(1, 500);
            Player player1 = new Player(2, 500);
            Player player2 = new Player(3, 500);
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.GetAllItems()).Returns(new List<Player>() { player, player1, player2 });

            var playerService = mock.Object;
            //Act
            playerService.CreateItem(player);
            playerService.CreateItem(player1);
            playerService.CreateItem(player2);
            var allItems = playerService.GetAllItems();
            //Assert
            Assert.Equal(3, allItems.Count());
        }
    }
}