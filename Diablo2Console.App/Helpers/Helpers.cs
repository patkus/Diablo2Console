using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

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

        public static string ReadFromJson(string filePath)
        {
            using StreamReader sr = new StreamReader(filePath);

            return sr.ReadToEnd();
        }

        public static void WriteToXml<T>(string directory, string filePath, List<T> itemList)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                Directory.CreateDirectory(directory);
                File.Create(directory + filePath).Dispose();
            }

            XmlRootAttribute root = new XmlRootAttribute
            {
                ElementName = "Items",
                IsNullable = true
            };
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>), root);

            using StreamWriter sw = new StreamWriter(directory + filePath);
            xmlSerializer.Serialize(sw, itemList);
        }

        public static List<T> ReadFromXml<T>(string filePath)
        {
            using StreamReader sr = new StreamReader(filePath);
            XmlRootAttribute root = new XmlRootAttribute
            {
                ElementName = "Items",
                IsNullable = true
            };
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>), root);

            var xmlItems = (List<T>)xmlSerializer.Deserialize(sr);

            return xmlItems;
        }
    }
}
