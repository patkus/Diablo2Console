using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Diablo2Console
{
    public class LevelService
    {
        public char[,] LoadLevelFromFile(string levelName)
        {
            string filePath = $"../../../files/{levelName}.txt";

            List<char[]> levelAsList = new List<char[]>();

            if (File.Exists(filePath))
            {
                string[] fileInLines = File.ReadAllLines(filePath);

                foreach (var textLine in fileInLines)
                {
                    levelAsList.Add(textLine.ToCharArray());
                }
            }
            else
            {
                Console.WriteLine($"No level file found.");
            }

            return JaggedIntoMultidimensionalArray(levelAsList.ToArray());
        }

        public char[,] JaggedIntoMultidimensionalArray(char[][] sourceArray)
        {
            int firstDim = sourceArray.Length;
            int secondDim;

            if (firstDim == 0)
            {
                secondDim = 0;
            }
            else
            {
                secondDim = sourceArray[0].Length;
            }

            char[,] resultArray = new char[firstDim, secondDim];

            for (int i = 0; i < firstDim; i++)
            {
                for (int j = 0; j < secondDim; j++)
                {
                    resultArray[i, j] = sourceArray[i][j];
                }
            }

            return resultArray;
        }

        public List<int> GetPlayerPosition(Level level)
        {
            List<int> playerPosition = new List<int>();

            for (int i = 0; i < level.Map.GetLength(0); i++)
            {
                for (int j = 0; j < level.Map.GetLength(1); j++)
                {
                    if (level.Map[i, j] == 'P')
                    {
                        playerPosition.Add(i);
                        playerPosition.Add(j);
                    }
                }
            }

            return playerPosition;
        }

        public void ChangePlayerPosition(List<int> oldPlayerPosition, List<int> newPlayerPosition, Level level, Player player, ActionMenuService actionMenuService)
        {
            Console.Clear();
            PlayerService playerService = new PlayerService();
            var charInNewPosition = level.Map[newPlayerPosition[0], newPlayerPosition[1]];
            if (charInNewPosition == ' ')
            {
                level.Map[newPlayerPosition[0], newPlayerPosition[1]] = 'P';
                level.Map[oldPlayerPosition[0], oldPlayerPosition[1]] = ' ';

                player.PositionX = newPlayerPosition[0];
                player.PositionY = newPlayerPosition[1];
              
                playerService.UpdatePlayersMap(player, level);
                playerService.DrawPlayerMap(player);
            }
            else if (charInNewPosition == 's')
            {
                playerService.UpdatePlayersMap(player, level);
                playerService.DrawPlayerMap(player);

                Smith smith = new Smith();
                Console.WriteLine(smith.Name);
                actionMenuService.PrintMenu(actionMenuService.GetAll("Smith"));
                var keyOperation = Console.ReadKey(true);
                bool selecting = true;
                while(selecting)
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
                playerService.UpdatePlayersMap(player, level);
                playerService.DrawPlayerMap(player);
            }
        }
    }
}
