using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.App.Helpers
{
    public static class Helper
    {
        public static char[,] JaggedIntoMultidimensionalArray(char[][] sourceArray)
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
    }
}
