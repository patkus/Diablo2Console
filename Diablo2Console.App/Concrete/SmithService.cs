using Diablo2Console.App.Common;
using Diablo2Console.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Diablo2Console.App.Concrete
{
    public class SmithService : BaseService<Smith>
    {
        private ActionMenuService _actionMenuService;

        public SmithService(ActionMenuService actionMenuService)
        {
            _actionMenuService = actionMenuService;
        }

        public override int CreateItem(Smith smith)
        {
            base.CreateItem(smith);

            if (smith.Name == "Charsie")
            {
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.C, "Chat", smith.Name));
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.R, "Repair", smith.Name));
                _actionMenuService.CreateItem(new ActionMenu(ConsoleKey.Escape, "Good bye", smith.Name));
            }

            return smith.Id;
        }
    }
}
