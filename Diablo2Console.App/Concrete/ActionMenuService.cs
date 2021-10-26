using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Diablo2Console.App.Concrete
{
    public class ActionMenuService : BaseService<ActionMenu>
    {
        public ActionMenuService()
        {
            Initialize();
        }
        public List<ActionMenu> GetMenuActionByGroup(string group)
        {
            List<ActionMenu> filteredActions = new List<ActionMenu>();
            foreach(var action in Items)
            {
                if(action.Group == group)
                {
                    filteredActions.Add(action);
                }
            }
            return filteredActions;
        }

        public void PrintMenu(List<ActionMenu> actionMenu)
        {
            foreach (var action in actionMenu)
            {
                Console.WriteLine($"{action.InputKey}: {action.Name}");
            }
        }
        private void Initialize()
        {
            CreateItem(new ActionMenu(ConsoleKey.Enter, "Start game", "Main"));
            CreateItem(new ActionMenu(ConsoleKey.S, "Top scores", "Main"));
            CreateItem(new ActionMenu(ConsoleKey.Escape, "Exit", "Main"));
            CreateItem(new ActionMenu(ConsoleKey.D1, "Normal", "Difficulty"));
            CreateItem(new ActionMenu(ConsoleKey.D2, "Nightmare", "Difficulty"));
            CreateItem(new ActionMenu(ConsoleKey.D3, "Hell", "Difficulty"));
            CreateItem(new ActionMenu(ConsoleKey.Escape, "Previous", "Difficulty"));
            CreateItem(new ActionMenu(ConsoleKey.C, "Chat", "Smith"));
            CreateItem(new ActionMenu(ConsoleKey.R, "Repair", "Smith"));
            CreateItem(new ActionMenu(ConsoleKey.Escape, "Good bye", "Smith"));
        }
    }
}
