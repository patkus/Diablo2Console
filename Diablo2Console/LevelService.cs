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
            var charInNewPosition = level.Map[newPlayerPosition[0], newPlayerPosition[1]];
            if (charInNewPosition == ' ')
            {
                level.Map[newPlayerPosition[0], newPlayerPosition[1]] = 'P';
                level.Map[oldPlayerPosition[0], oldPlayerPosition[1]] = ' ';

                player.PositionX = newPlayerPosition[0];
                player.PositionY = newPlayerPosition[1];
            }
            else if (charInNewPosition != 'x')
            {
                Smith smith = new Smith(actionMenuService);
            }

            Console.Clear();
            player.PlayerMap[player.PositionX, player.PositionY] = 'P';
            for(int i = player.PositionX - 1; i <= player.PositionX + 1; i++)
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
    }
}
