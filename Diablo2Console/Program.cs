using Diablo2Console.App.Concrete;
using Diablo2Console.App.Managers;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diablo2Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool playing = true;
           
            Console.WriteLine("Welcome in the Diablo II Console world!\n");
            Console.WriteLine("Select what you want to do:\n");

            while (playing)
            {
                ActionMenuService actionMenuService = new ActionMenuService();
                var mainMenu = actionMenuService.GetMenuActionByGroup("Main");
                
                MonsterService monsterService = new MonsterService();
                ItemService itemService = new ItemService();
                PlayerService playerService = new PlayerService(itemService);
                Player player = new Player(playerService.GetNextId(), PlayerService.PlayerBasicHealth); 

                actionMenuService.PrintMenu(mainMenu);
                Console.WriteLine();

                LevelService levelService = new LevelService();
                LevelManager levelManager = new LevelManager(levelService);

                var keyOperation = Console.ReadKey(true);

                switch (keyOperation.Key)
                {
                    case ConsoleKey.Enter:
                        playerService.CreateItem(player);
                        SelectDifficulty(actionMenuService, monsterService, playerService, player, levelService, levelManager);
                        break;
                    case ConsoleKey.L:
                        playerService.LoadItem(player);
                        var playerManager = InitializeLevelAndPlayer(actionMenuService, monsterService, playerService, player, levelService, levelManager);
                        playerManager.PlayLevel(player, player.PositionX, player.PositionY);
                        break;
                    case ConsoleKey.Escape:
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("Wrong operation, choose another one.");
                        break;
                }
            }
        }

        private static void SelectDifficulty(ActionMenuService actionMenuService, MonsterService monsterService, PlayerService playerService, Player player, LevelService levelService, LevelManager levelManager)
        {
            var difficultyMenu = actionMenuService.GetMenuActionByGroup("Difficulty");
            actionMenuService.PrintMenu(difficultyMenu);
            ConsoleKeyInfo keyOperation = Console.ReadKey(true);
            bool selectingDifficulty = true;

            while (selectingDifficulty)
            {
                if (keyOperation.Key == ConsoleKey.D1)
                {
                    var playerManager = InitializeLevelAndPlayer(actionMenuService, monsterService, playerService, player, levelService, levelManager);
                    selectingDifficulty = false;

                    playerManager.PlayLevel(player, player.PositionX, player.PositionY);
                }
                else if (keyOperation.Key == ConsoleKey.Escape)
                {
                    selectingDifficulty = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong operation, choose another one.");
                    keyOperation = Console.ReadKey(true);
                }
            }
        }

        private static PlayerManager InitializeLevelAndPlayer(ActionMenuService actionMenuService, MonsterService monsterService, PlayerService playerService, Player player, LevelService levelService, LevelManager levelManager)
        {
            PlayerManager playerManager = new PlayerManager(playerService, levelService, actionMenuService, monsterService);
            var newLevelId = levelManager.AddNewLevel("Level1");
            var playerPosition = new List<int>();

            if (player.PlayerMap is null)
            {
                if (newLevelId != -1)
                {
                    var levelMap = levelService.GetAllItems().Where(x => x.CurrentlyPlaying == true).FirstOrDefault();
                    playerPosition = playerManager.GetPlayerPosition(levelMap.Map);
                    if (playerPosition.Count > 0)
                    {
                        player.PositionX = playerPosition[0];
                        player.PositionY = playerPosition[1];
                        playerManager.SetStartingMapForPlayer();
                    }
                }
            }
            else
            {
                playerPosition = playerManager.GetPlayerPosition(player.PlayerMap);
                if (playerPosition.Count > 0)
                {
                    player.PositionX = playerPosition[0];
                    player.PositionY = playerPosition[1];
                }
            }
            
            playerManager.DrawPlayerMap();

            return playerManager;
        }
    }
}
