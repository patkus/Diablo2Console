using Diablo2Console.App.Abstract;
using Diablo2Console.App.Concrete;
using Diablo2Console.App.Managers;
using Diablo2Console.Domain.Entity;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

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
        public void RemoveItemValidCall()
        {
            //Arrange
            Player playerToRemove = GetSampleData()[0];
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.RemoveItem(playerToRemove));

            var playerService = mock.Object;
            //Act
            playerService.RemoveItem(playerToRemove);
            //Assert
            mock.Verify(x => x.RemoveItem(playerToRemove), Times.Once());
        }

        [Fact]
        public void GetAllItemsValidReturn()
        {
            //Arrange
            var expectedOutcome = GetSampleData();
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.GetAllItems()).Returns(GetSampleData());

            var playerService = mock.Object;
            //Act        
            var actualOutcome = playerService.GetAllItems();
            //Assert
            Assert.NotNull(actualOutcome);
            Assert.Equal(expectedOutcome.Count(), actualOutcome.Count());

            for(int i = 0; i < expectedOutcome.Count(); i++)
            {
                Assert.Equal(expectedOutcome[i].Id, actualOutcome[i].Id);
            }
        }

        [Fact]
        public void UpdateItemValidCall()
        {
            //Arrange
            Player currentPlayer = GetSampleData()[0];
            
            var mock = new Mock<IService<Player>>();
            mock.Setup(s => s.UpdateItem(currentPlayer)).Returns(1);

            var playerService = mock.Object;
            //Act
            playerService.UpdateItem(currentPlayer);
            //Assert
            mock.Verify(x => x.UpdateItem(currentPlayer), Times.Once());
        }

        private List<Player> GetSampleData()
        {
            return new List<Player>() { new Player(1, 100), new Player(2, 200), new Player(3, 300) };
        }
    }
}