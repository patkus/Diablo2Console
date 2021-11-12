using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diablo2Console.App.Concrete
{
    public class NpcService : BaseService<Npc>
    {
        private ActionMenuService _actionMenuService;

        public NpcService(ActionMenuService actionMenuService)
        {
            _actionMenuService = actionMenuService;
        }

        public override int CreateItem(Npc npc)
        {
            base.CreateItem(npc);

            if (npc.Name == "Charsie")
            {
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.C, "Chat", npc.Name));
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.R, "Repair", npc.Name));
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.Escape, "Good bye", npc.Name));
            }
            else if(npc.Name == "Akara")
            {
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.C, "Chat", npc.Name));
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.H, "Heal", npc.Name));
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.Escape, "Good bye", npc.Name));
            }

            return npc.Id;
        }
    }
}
