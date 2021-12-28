using Diablo2Console.App.Concrete;
using Diablo2Console.App.Managers;
using Diablo2Console.Domain.Entity;
using System;

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
                ItemService itemService = new ItemService();
                MonsterService monsterService = new MonsterService();
                PlayerService playerService = new PlayerService(itemService);
                Player player = new Player(PlayerService.PlayerBasicHealth);
                playerService.CreateItem(player);

                actionMenuService.PrintMenu(mainMenu);
                Console.WriteLine();

                int oldPlayerPositionX;
                int oldPlayerPositionY;

                LevelService levelService = new LevelService();
                LevelManager levelManager = new LevelManager(levelService);

                var keyOperation = Console.ReadKey(true);

                switch (keyOperation.Key)
                {
                    case ConsoleKey.Enter:
                        var difficultyMenu = actionMenuService.GetMenuActionByGroup("Difficulty");
                        actionMenuService.PrintMenu(difficultyMenu);
                        keyOperation = Console.ReadKey(true);

                        bool selectingDifficulty = true;

                        while (selectingDifficulty)
                        {
                            if (keyOperation.Key == ConsoleKey.D1)
                            {
                                var newLevelId = levelManager.AddNewLevel("Level1");
                                PlayerManager playerManager = new PlayerManager(playerService, levelService, actionMenuService, monsterService);

                                if (newLevelId != -1)
                                {
                                    var playerPosition = levelService.GetPlayerPosition();
                                    if (playerPosition.Count > 0)
                                    {
                                        player.PositionX = playerPosition[0];
                                        player.PositionY = playerPosition[1];
                                       
                                        playerManager.SetStartingMapForPlayer();
                                        playerManager.DrawPlayerMap();
                                    }
                                }
                                selectingDifficulty = false;

                                bool playingLevel = true;

                                while(playingLevel)
                                {
                                    keyOperation = Console.ReadKey(true);

                                    oldPlayerPositionX = player.PositionX;
                                    oldPlayerPositionY = player.PositionY;

                                    switch (keyOperation.Key)
                                    {
                                        case ConsoleKey.RightArrow:
                                            playerManager.ChangePlayerPosition(oldPlayerPositionX, oldPlayerPositionY, oldPlayerPositionX, ++oldPlayerPositionY);
                                            break;
                                        case ConsoleKey.LeftArrow:
                                            playerManager.ChangePlayerPosition(oldPlayerPositionX, oldPlayerPositionY, oldPlayerPositionX, --oldPlayerPositionY);
                                            break;
                                        case ConsoleKey.UpArrow:
                                            playerManager.ChangePlayerPosition(oldPlayerPositionX, oldPlayerPositionY, --oldPlayerPositionX, oldPlayerPositionY);
                                            break;
                                        case ConsoleKey.DownArrow:
                                            playerManager.ChangePlayerPosition(oldPlayerPositionX, oldPlayerPositionY, ++oldPlayerPositionX, oldPlayerPositionY);
                                            break;
                                        case ConsoleKey.I:
                                            playerManager.ShowPlayerBag();
                                            
                                            bool showingPlayerBag = true;

                                            while(showingPlayerBag)
                                            {
                                                keyOperation = Console.ReadKey(true);

                                                switch(keyOperation.Key)
                                                {
                                                    case ConsoleKey.Escape:
                                                        Console.Clear();
                                                        showingPlayerBag = false;
                                                        playerManager.DrawPlayerMap();
                                                        break;
                                                    default:
                                                        Console.WriteLine("Wrong operation, choose another one.");
                                                        break;
                                                }
                                            }
                                            break;
                                        case ConsoleKey.Escape:
                                            playingLevel = false;
                                            Console.Clear();
                                            break;
                                        default:
                                            Console.WriteLine("Wrong operation, choose another one.");
                                            break;
                                    }
                                }
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
    }
}
