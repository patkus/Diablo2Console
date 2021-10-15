using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console
{
    public class PlayerService
    {
        public void DrawPlayerMap(Player player)
        {
            for (int i = 0; i < player.PlayerMap.GetLength(0); i++)
            {
                for (int j = 0; j < player.PlayerMap.GetLength(1); j++)
                {
                    Console.Write(player.PlayerMap[i, j]);
                }
                Console.WriteLine();
            }
        }
        public char[,] SetStartingMapForPlayer(Level level, Player player)
        {
            int levelXDim = level.Map.GetLength(0);
            int levelYDim = level.Map.GetLength(1);

            char[,] playerMap = new char[levelXDim, levelYDim];

            for (int i = 0; i < levelXDim; i++)
            {
                for (int j = 0; j < levelYDim; j++)
                {
                    if (i <= player.PositionX + 1 && i >= player.PositionX - 1 && j <= player.PositionY + 1 && j >= player.PositionY - 1)
                    {
                        playerMap[i,j] = level.Map[i,j];
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

            return playerMap;
        }
    }
}
