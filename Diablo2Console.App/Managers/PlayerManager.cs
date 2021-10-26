using Diablo2Console.App.Concrete;
using Diablo2Console.Domain.Entity;
using System;
using System.Linq;

namespace Diablo2Console.App.Managers
{
    public class PlayerManager
    {
        private PlayerService _playerService;
        private LevelService _levelService;
        private ActionMenuService _actionMenuService;

        public PlayerManager(PlayerService playerService, LevelService levelService, ActionMenuService actionMenuService)
        {
            _playerService = playerService;
            _levelService = levelService;
            _actionMenuService = actionMenuService;
        }
        public void DrawPlayerMap()
        {
            var player = _playerService.GetAllItems().FirstOrDefault();
            for (int i = 0; i < player.PlayerMap.GetLength(0); i++)
            {
                for (int j = 0; j < player.PlayerMap.GetLength(1); j++)
                {
                    Console.Write(player.PlayerMap[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void SetStartingMapForPlayer()
        {
            var player = _playerService.GetAllItems().FirstOrDefault();
            var level = _levelService.GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);

            int levelXDim = level.Map.GetLength(0);
            int levelYDim = level.Map.GetLength(1);

            char[,] playerMap = new char[levelXDim, levelYDim];

            for (int i = 0; i < levelXDim; i++)
            {
                for (int j = 0; j < levelYDim; j++)
                {
                    if (i <= player.PositionX + 1 && i >= player.PositionX - 1 && j <= player.PositionY + 1 && j >= player.PositionY - 1)
                    {
                        playerMap[i, j] = level.Map[i, j];
                    }
                    else if (i == 0 || i == levelXDim - 1 || j == 0 || j == levelYDim - 1)
                    {
                        playerMap[i, j] = 'x';
                    }
                    else
                    {
                        playerMap[i, j] = ' ';
                    }
                }
            }

            player.PlayerMap = playerMap;
        }
        public void UpdatePlayersMap()
        {
            var player = _playerService.GetAllItems().FirstOrDefault();
            var level = _levelService.GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);

            player.PlayerMap[player.PositionX, player.PositionY] = 'P';
            for (int i = player.PositionX - 1; i <= player.PositionX + 1; i++)
            {
                for (int j = player.PositionY - 1; j <= player.PositionY + 1; j++)
                {
                    if ((i, j) != (player.PositionX, player.PositionY))
                    {
                        player.PlayerMap[i, j] = level.Map[i, j];
                    }
                }
            }
        }
        public void ChangePlayerPosition(int oldPlayerPositionX, int oldPlayerPositionY, int newPlayerPositionX, int newPlayerPositionY)
        {
            Console.Clear();

            var player = _playerService.GetAllItems().FirstOrDefault();
            var level = _levelService.GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);

            var charInNewPosition = level.Map[newPlayerPositionX, newPlayerPositionY];
            if (charInNewPosition == ' ')
            {
                level.Map[newPlayerPositionX, newPlayerPositionY] = 'P';
                level.Map[oldPlayerPositionX, oldPlayerPositionY] = ' ';

                player.PositionX = newPlayerPositionX;
                player.PositionY = newPlayerPositionY;

                UpdatePlayersMap();
                DrawPlayerMap();
            }
            else if (charInNewPosition == 's')
            {
                UpdatePlayersMap();
                DrawPlayerMap();

                Smith smith = new Smith();
                Console.WriteLine(smith.Name);
                _actionMenuService.PrintMenu(_actionMenuService.GetMenuActionByGroup("Smith"));
                var keyOperation = Console.ReadKey(true);
                bool selecting = true;
                while (selecting)
                {
                    switch (keyOperation.Key)
                    {
                        case ConsoleKey.R:
                            Console.WriteLine(smith.SpokenLines["Repair"]);
                            keyOperation = Console.ReadKey(true);
                            break;
                        case ConsoleKey.C:
                            Console.WriteLine(smith.SpokenLines["Chat"]);
                            keyOperation = Console.ReadKey(true);
                            break;
                        case ConsoleKey.Escape:
                            Console.WriteLine(smith.SpokenLines["GoodBye"]);
                            selecting = false;
                            break;
                        default:
                            Console.WriteLine("Wrong operation, choose another one.");
                            keyOperation = Console.ReadKey(true);
                            break;
                    }
                }
            }
            else
            {
                UpdatePlayersMap();
                DrawPlayerMap();
            }
        }
    }
}
