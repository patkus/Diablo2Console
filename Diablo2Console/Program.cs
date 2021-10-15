using System;
using System.Collections.Generic;

namespace Diablo2Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool playing = true;
            ActionMenuService actionMenuService = new ActionMenuService();
            Initialize(actionMenuService);

            Console.WriteLine("Welcome in the Diablo II Console world!\n");
            Console.WriteLine("Select what you want to do:\n");

            var mainMenu = actionMenuService.GetAll("Main");
            PlayerService playerService = new PlayerService();
            Player player = new Player();
            LevelService levelService = new LevelService();
            Level currentLevel = new Level();

            actionMenuService.PrintMenu(mainMenu);
            Console.WriteLine();

            while (playing)
            {
                List<int> oldPlayerPosition = new List<int>() { player.PositionX, player.PositionY };
                List<int> newPlayerPosition = new List<int>() { player.PositionX, player.PositionY };

                var keyOperation = Console.ReadKey(true);

                switch (keyOperation.Key)
                {
                    case ConsoleKey.Enter:
                        var difficultyMenu = actionMenuService.GetAll("Difficulty");
                        actionMenuService.PrintMenu(difficultyMenu);
                        keyOperation = Console.ReadKey(true);

                        bool selectingDifficulty = true;

                        while (selectingDifficulty)
                        {
                            if (keyOperation.Key == ConsoleKey.D1)
                            {
                                Console.Clear();
                                currentLevel.Map = levelService.LoadLevelFromFile("Level1");
                                if (currentLevel.Map != null)
                                {
                                    var playerPosition = levelService.GetPlayerPosition(currentLevel);
                                    if (playerPosition.Count > 0)
                                    {
                                        player.PositionX = playerPosition[0];
                                        player.PositionY = playerPosition[1];

                                        player.PlayerMap = playerService.SetStartingMapForPlayer(currentLevel, player);
                                        playerService.DrawPlayerMap(player);
                                    }
                                }
                                selectingDifficulty = false;
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
                    case ConsoleKey.RightArrow:
                        newPlayerPosition[1]++;
                        levelService.ChangePlayerPosition(oldPlayerPosition, newPlayerPosition, currentLevel, player, actionMenuService);
                        break;
                    case ConsoleKey.LeftArrow:
                        newPlayerPosition[1]--;
                        levelService.ChangePlayerPosition(oldPlayerPosition, newPlayerPosition, currentLevel, player, actionMenuService);
                        break;
                    case ConsoleKey.UpArrow:
                        newPlayerPosition[0]--;
                        levelService.ChangePlayerPosition(oldPlayerPosition, newPlayerPosition, currentLevel, player, actionMenuService);
                        break;
                    case ConsoleKey.DownArrow:
                        newPlayerPosition[0]++;
                        levelService.ChangePlayerPosition(oldPlayerPosition, newPlayerPosition, currentLevel, player, actionMenuService);
                        break;
                    case ConsoleKey.Escape:
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("Wrong operation, choose another one.");
                        break;
                }
                Console.WriteLine();
            }
        }

        public static void Initialize(ActionMenuService actionMenuService)
        {
            actionMenuService.AddNewActionMenu(ConsoleKey.Enter, "Start game", "Main");
            actionMenuService.AddNewActionMenu(ConsoleKey.S, "Top scores", "Main");
            actionMenuService.AddNewActionMenu(ConsoleKey.Escape, "Exit", "Main");
            actionMenuService.AddNewActionMenu(ConsoleKey.D1, "Normal", "Difficulty");
            actionMenuService.AddNewActionMenu(ConsoleKey.D2, "Nightmare", "Difficulty");
            actionMenuService.AddNewActionMenu(ConsoleKey.D3, "Hell", "Difficulty");
            actionMenuService.AddNewActionMenu(ConsoleKey.Escape, "Previous", "Difficulty");
            actionMenuService.AddNewActionMenu(ConsoleKey.C, "Chat", "Smith");
            actionMenuService.AddNewActionMenu(ConsoleKey.R, "Repair", "Smith");
            actionMenuService.AddNewActionMenu(ConsoleKey.Escape, "Good bye", "Smith");
        }
    }
}
