using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Diablo2Console.App.Concrete
{
    public class LevelService : BaseService<Level>
    {
        public List<int> GetPlayerPosition()
        {
            var level = GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);
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
