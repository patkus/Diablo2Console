using System;

namespace Diablo2Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool playing = true;
            ActionMenuService actionMenuService = new ActionMenuService();
            Initialize(actionMenuService);

            Console.WriteLine("Welcome in the Diablo II Console world!\n");
            Console.WriteLine("Select what you want to do:\n");

            var mainMenu = actionMenuService.GetAll("Main");

            while (playing)
            {
                actionMenuService.PrintMenu(mainMenu);
                Console.WriteLine();
                var keyOperation = Console.ReadKey(true);

                switch (keyOperation.Key)
                {
                    case ConsoleKey.Enter:
                        var difficultyMenu = actionMenuService.GetAll("Difficulty");
                        actionMenuService.PrintMenu(difficultyMenu);
                        keyOperation = Console.ReadKey(true);

                        bool selectingDifficulty = true;

                        while (selectingDifficulty)
                        {
                            if (keyOperation.Key == ConsoleKey.D1)
                            {
                                Console.Clear();
                                LevelService levelService = new LevelService();
                                Level firstLevel = new Level(map: new char[10, 20] { { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' }, { 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x', 'x' } });
                                levelService.DrawLevelInConsole(firstLevel.Map);
                                keyOperation = Console.ReadKey();
                            }
                            else if (keyOperation.Key == ConsoleKey.Escape)
                            {
                                selectingDifficulty = false;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Wrong operation, choose another one.");
                                keyOperation = Console.ReadKey(true);
                            }
                        }
                        break;
                    case ConsoleKey.Escape:
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("Wrong operation, choose another one.");
                        break;
                }
                Console.WriteLine();
            }
            /*
            bool playing = true;
            var player = new Player(1, 1);
            var levelMap = new char[,] { { 'x', 'x', 'x', 'x', 'x' }, { 'x', 'o', ' ', ' ', 'x' }, { 'x', ' ', ' ', ' ', 'x' }, { 'x', ' ', ' ', ' ', 'x' }, { 'x', 'x', 'x', 'x', 'x' } };
            DrawMap(levelMap);
            while (playing)
            {
                var action = Console.ReadKey();
                switch (action.Key)
                {
                    case ConsoleKey.RightArrow:
                        levelMap[player.PositionX, player.PositionY] = ' ';
                        player.PositionY++;
                        levelMap[player.PositionX, player.PositionY] = 'o';
                        break;
                    case ConsoleKey.Escape:
                        playing = false;
                        break;
                    default:
                        break;
                }
                Console.Clear();
                DrawMap(levelMap);
            }*/
        }
        public static void DrawMap(char[,] levelMap)
        {
            for (int i = 0; i <= levelMap.GetLength(0) - 1; i++)
            {
                for (int j = 0; j <= levelMap.GetLength(1) - 1; j++)
                {
                    Console.Write(levelMap[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void Initialize(ActionMenuService actionMenuService)
        {
            actionMenuService.AddNewActionMenu(ConsoleKey.Enter, "Start game", "Main");
            actionMenuService.AddNewActionMenu(ConsoleKey.S, "Top scores", "Main");
            actionMenuService.AddNewActionMenu(ConsoleKey.Escape, "Exit", "Main");
            actionMenuService.AddNewActionMenu(ConsoleKey.D1, "Normal", "Difficulty");
            actionMenuService.AddNewActionMenu(ConsoleKey.D2, "Nightmare", "Difficulty");
            actionMenuService.AddNewActionMenu(ConsoleKey.D3, "Hell", "Difficulty");
            actionMenuService.AddNewActionMenu(ConsoleKey.Escape, "Previous", "Difficulty");
        }
    }
}
