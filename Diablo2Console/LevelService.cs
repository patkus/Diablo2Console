using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console
{
    public class LevelService
    {
        public void DrawLevelInConsole(char[,] levelMap)
        {
            for (int i = 0; i < levelMap.GetLength(0); i++)
            {
                for (int j = 0; j < levelMap.GetLength(1); j++)
                {
                    Console.Write(levelMap[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
