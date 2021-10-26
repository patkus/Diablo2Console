using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diablo2Console.App.Concrete;
using Diablo2Console.Domain.Entity;

namespace Diablo2Console.App.Managers
{
    public class LevelManager
    {
        private LevelService _levelService;

        public LevelManager(LevelService levelService)
        {
            _levelService = levelService;
        }
        public int AddNewLevel(string levelName)
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
                return -1;
            }

            var levelId = _levelService.GetNextId();
            Level level = new Level(levelId, levelName, JaggedIntoMultidimensionalArray(levelAsList.ToArray()));
            _levelService.CreateItem(level);

            return level.Id;
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
        public List<int> GetPlayerPosition()
        {
            var level = _levelService.GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);
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
    }
}
