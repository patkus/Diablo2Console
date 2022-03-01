using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diablo2Console.App.Abstract;
using Diablo2Console.App.Concrete;
using Diablo2Console.App.Helpers;
using Diablo2Console.Domain.Entity;

namespace Diablo2Console.App.Managers
{
    public class LevelManager
    {
        private readonly IService<Level> _levelService;

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
            Level level = new Level(levelId, levelName, Helper.JaggedIntoMultidimensionalArray(levelAsList.ToArray()));
            _levelService.CreateItem(level);

            return level.Id;
        }
    }
}
