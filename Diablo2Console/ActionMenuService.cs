using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console
{
    public class ActionMenuService
    {
        private List<ActionMenu> actions = new List<ActionMenu>();

        public void AddNewActionMenu(ConsoleKey inputKey, string name, string group)
        {
            ActionMenu actionMenu = new ActionMenu() { InputKey = inputKey, Name = name, Group = group};
            actions.Add(actionMenu);
        }

        public List<ActionMenu> GetAll(string group)
        {
            List<ActionMenu> filteredActions = new List<ActionMenu>();
            foreach(var action in actions)
            {
                if(action.Group == group)
                {
                    filteredActions.Add(action);
                }
            }
            return filteredActions;
        }

        public void PrintMenu(List<ActionMenu> actionMenus)
        {
            foreach (var action in actionMenus)
            {
                Console.WriteLine($"{action.InputKey}: {action.Name}");
            }
        }
    }
}
