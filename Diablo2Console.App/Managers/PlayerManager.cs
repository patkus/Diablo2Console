using Diablo2Console.App.Concrete;
using Diablo2Console.Domain.Entity;
using System;
using System.Linq;

namespace Diablo2Console.App.Managers
{
    public class PlayerManager
    {
        private PlayerService _playerService;
        private LevelService _levelService;
        private ActionMenuService _actionMenuService;
        private NpcService _npcService;
        private QuestService _questService;
        private TaskService _taskService;
        private TaskFunctionService _taskFunctionService;
        private MonsterService _monsterService;

        public PlayerManager(PlayerService playerService, LevelService levelService, ActionMenuService actionMenuService, MonsterService monsterService)
        {
            _playerService = playerService;
            _levelService = levelService;
            _actionMenuService = actionMenuService;
            _npcService = new NpcService(_actionMenuService);
            _questService = new QuestService(levelService);
            _taskService = new TaskService(_questService);
            _taskFunctionService = new TaskFunctionService(_taskService);
            _monsterService = monsterService;

        }
        public void DrawPlayerMap()
        {
            Console.Clear();
            var player = _playerService.GetAllItems().FirstOrDefault();
            for (int i = 0; i < player.PlayerMap.GetLength(0); i++)
            {
                for (int j = 0; j < player.PlayerMap.GetLength(1); j++)
                {
                    Console.Write(player.PlayerMap[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void SetStartingMapForPlayer()
        {
            var player = _playerService.GetAllItems().FirstOrDefault();
            var level = _levelService.GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);

            int levelXDim = level.Map.GetLength(0);
            int levelYDim = level.Map.GetLength(1);

            char[,] playerMap = new char[levelXDim, levelYDim];

            for (int i = 0; i < levelXDim; i++)
            {
                for (int j = 0; j < levelYDim; j++)
                {
                    if (i <= player.PositionX + 1 && i >= player.PositionX - 1 && j <= player.PositionY + 1 && j >= player.PositionY - 1)
                    {
                        playerMap[i, j] = level.Map[i, j];
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

            player.PlayerMap = playerMap;
        }
        public void UpdatePlayersMap()
        {
            var player = _playerService.GetAllItems().FirstOrDefault();
            var level = _levelService.GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);

            player.PlayerMap[player.PositionX, player.PositionY] = 'P';
            for (int i = player.PositionX - 1; i <= player.PositionX + 1; i++)
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
        public void ChangePlayerPosition(int oldPlayerPositionX, int oldPlayerPositionY, int newPlayerPositionX, int newPlayerPositionY)
        {
            var player = _playerService.GetAllItems().FirstOrDefault();
            var level = _levelService.GetAllItems().FirstOrDefault(x => x.CurrentlyPlaying == true);

            var charInNewPosition = level.Map[newPlayerPositionX, newPlayerPositionY];
            if (charInNewPosition == ' ')
            {
                level.Map[newPlayerPositionX, newPlayerPositionY] = 'P';
                level.Map[oldPlayerPositionX, oldPlayerPositionY] = ' ';

                player.PositionX = newPlayerPositionX;
                player.PositionY = newPlayerPositionY;

                UpdatePlayersMap();
                DrawPlayerMap();
            }
            else if (charInNewPosition == 's')
            {
                var smith = _npcService.GetAllItems().Where(x => x.Type == "Smith" && x.LevelId == level.Id).FirstOrDefault();
                if (smith == null)
                {
                    string smithName = "";
                    if (level.Name == "Level1")
                    {
                        smithName = "Charsie";
                    }
                    smith = new Npc(_npcService.GetNextId(), smithName, "Smith", level.Id);
                    _npcService.CreateItem(smith);
                }
                Console.WriteLine(smith.Name);
                _actionMenuService.PrintMenu(_actionMenuService.GetMenuActionByGroup(smith.Name));
                var keyOperation = Console.ReadKey(true);
                bool selecting = true;
                while (selecting)
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
            else if (charInNewPosition == 'h')
            {
                var healer = _npcService.GetAllItems().Where(x => x.Type == "Healer" && x.LevelId == level.Id).FirstOrDefault();
                if (healer == null)
                {
                    string healerName = "";
                    if (level.Name == "Level1")
                    {
                        healerName = "Akara";
                    }
                    healer = new Npc(_npcService.GetNextId(), healerName, "Healer", level.Id);
                    _npcService.CreateItem(healer);
                }

                //The Den of Evil - Task1
                var theDenOfEvilFinished = _questService.GetAllItems().Where(x => x.Name == "The Den of Evil").FirstOrDefault();
                if (!theDenOfEvilFinished.Finished)
                {
                    var activeTask = _taskService.GetAllItems().Where(x => x.QuestId == theDenOfEvilFinished.Id && x.Active == true).FirstOrDefault();
                    if (activeTask != null)
                    {
                        _taskFunctionService.SpeakToAkara();
                        Console.WriteLine(healer.SpokenLines[activeTask.Name]);
                        Console.WriteLine("Enter - Continue playing");

                        bool readingTask = true;
                        while (readingTask)
                        {
                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                readingTask = false;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(healer.Name);
                    _actionMenuService.PrintMenu(_actionMenuService.GetMenuActionByGroup(healer.Name));
                    var keyOperation = Console.ReadKey(true);
                    bool selecting = true;
                    while (selecting)
                    {
                        switch (keyOperation.Key)
                        {
                            case ConsoleKey.H:
                                player.Health = PlayerService.PlayerBasicHealth;
                                Console.WriteLine(healer.SpokenLines["Heal"]);
                                keyOperation = Console.ReadKey(true);
                                break;
                            case ConsoleKey.C:
                                Console.WriteLine(healer.SpokenLines["Chat"]);
                                keyOperation = Console.ReadKey(true);
                                break;
                            case ConsoleKey.Escape:
                                Console.WriteLine(healer.SpokenLines["GoodBye"]);
                                selecting = false;
                                break;
                            default:
                                Console.WriteLine("Wrong operation, choose another one.");
                                keyOperation = Console.ReadKey(true);
                                break;
                        }
                    }
                }

                UpdatePlayersMap();
                DrawPlayerMap();
            }
            else if (charInNewPosition == 'f' || charInNewPosition == 'F')
            {
                MonsterEncounter(charInNewPosition, level, player, newPlayerPositionX, newPlayerPositionY);
            }
            else
            {
                UpdatePlayersMap();
                DrawPlayerMap();
            }
        }
        private void MonsterEncounter(char charInNewPosition, Level level, Player player, int newPlayerPositionX, int newPlayerPositionY)
        {
            if (GetMonsterInfo(charInNewPosition, level))
            {
                FightWithMonster(charInNewPosition, level, player, newPlayerPositionX, newPlayerPositionY);
            }
            else
            {
                DrawPlayerMap();
            }
        }
        private bool GetMonsterInfo(char monsterMapSymbol, Level level)
        {
            var monster = _monsterService.GetAllItems().Where(x => x.MapSymbol == monsterMapSymbol && x.LevelIdsAppearance.Contains(level.Name)).FirstOrDefault();

            Console.WriteLine($"Monster name: {monster.Name}");
            Console.WriteLine($"Monster health: {monster.Health}");
            Console.WriteLine($"Monster minimal damage: {monster.MinDamage}");
            Console.WriteLine($"Monster maximal damage: {monster.MaxDamage}");

            _actionMenuService.PrintMenu(_actionMenuService.GetMenuActionByGroup("MonsterInfo"));
            bool selecting = true;
            while(selecting)
            {
                var keyOperation = Console.ReadKey(true);
                switch (keyOperation.Key)
                {
                    case ConsoleKey.F:
                        return true;
                    case ConsoleKey.Escape:
                        return false;
                    default:
                        Console.WriteLine("Wrong operation, choose another one.");
                        selecting = true;
                        break;
                }
            }

            return false;
        }
        private void FightWithMonster(char monsterMapSymbol, Level level, Player player, int monsterPositionX, int monsterPositionY)
        {
            SetPlayerDamage(player);
            SetPlayerArmor(player);
            var monster = _monsterService.GetAllItems().Where(x => x.MapSymbol == monsterMapSymbol && x.LevelIdsAppearance.Contains(level.Name)).FirstOrDefault();

            if(monster != null)
            {                
                int playerHealth = player.Health;
                int monsterHealth = monster.Health;
                bool fighting = true;

                while(fighting)
                {
                    var rand = new Random();
                    int monsterDamage = rand.Next(monster.MinDamage, monster.MaxDamage);
                    int playerDamage = rand.Next(player.SummedMinDamage, player.SummedMaxDamage);
                    Console.WriteLine($"Player health: {playerHealth}");
                    Console.WriteLine($"Monster health: {monsterHealth}");

                    _actionMenuService.PrintMenu(_actionMenuService.GetMenuActionByGroup("FightWithMonster"));
                    var keyOperation = Console.ReadKey(true);
                    switch(keyOperation.Key)
                    {
                        case ConsoleKey.A:
                            monsterHealth -= playerDamage;
                            if (monsterHealth <= 0)
                            {
                                fighting = false;
                                player.PlayerMap[monsterPositionX, monsterPositionY] = ' ';
                                level.Map[monsterPositionX, monsterPositionY] = ' ';
                            }
                            else
                            {
                                playerHealth = AttackPlayer(playerHealth, player, monsterDamage, level, out fighting);
                            }
                            break;
                        case ConsoleKey.B:
                            playerHealth = AttackPlayer(playerHealth, player, monsterDamage, level, out fighting);
                            break;
                        case ConsoleKey.R:
                            fighting = false;
                            break;
                        default:
                            Console.WriteLine("Wrong operation, choose another one.");
                            break;
                    }
                }
                player.Health = playerHealth;
                DrawPlayerMap();
            }
        }
        private int AttackPlayer(int playerHealth, Player player, int monsterDamage, Level level, out bool fighting)
        {
            playerHealth -= monsterDamage - (int)(0.1 * player.SummedArmor);
            if (playerHealth <= 0)
            {
                int currentPlayerPostionX = player.PositionX;
                int currentPlayerPostionY = player.PositionY;
                ChangePlayerPosition(currentPlayerPostionX, currentPlayerPostionY, level.PlayerStartingPositionX, level.PlayerStartingPositionY);
                player.PlayerMap[currentPlayerPostionX, currentPlayerPostionY] = ' ';
                level.Map[currentPlayerPostionX, currentPlayerPostionY] = ' ';
                fighting = false;

                return PlayerService.PlayerBasicHealth;
            }

            fighting = true;

            return playerHealth;
        }
        public void ShowPlayerBag()
        {
            var player = _playerService.GetAllItems().FirstOrDefault();

            Console.WriteLine();
            foreach (var item in player.PlayerBag)
            {
                Console.WriteLine("Bag: ");
                Console.WriteLine();
                item.ShowItem();              
            }
            Console.WriteLine();
            _actionMenuService.PrintMenu(_actionMenuService.GetMenuActionByGroup("PlayerBag"));
        }

        public void SetPlayerDamage(Player player)
        {
            player.SummedMinDamage = 0;
            player.SummedMaxDamage = 0;
            foreach (OffensiveItem item in player.PlayerBag.Where(x => x.ItemType == "OffensiveItem"))
            {
                player.SummedMinDamage += item.MinDamage;
                player.SummedMaxDamage += item.MaxDamage;
            }
        }

        public void SetPlayerArmor(Player player)
        {
            player.SummedArmor = 0;
            foreach (DefensiveItem item in player.PlayerBag.Where(x => x.ItemType == "DefensiveItem"))
            {
                player.SummedArmor += item.Armor;
            }
        }
    }
}
