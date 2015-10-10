using System;
using System.Collections.Generic;
using Ryanstaurant.OMS.Interface;
using Ryanstaurant.OMS.DataContract;
using Ryanstaurant.OMS.WorkSpace;

namespace Ryanstaurant.OMS.Service
{
    public class MenuService:IMenuService
    {
        private readonly BllMenu bllMenu = new BllMenu();


        public IList<Menu> GetMenus()
        {
            return bllMenu.GetMenus();
        }

        public Menu AddMenu(Menu menu)
        {
            return bllMenu.AddMenu(menu);
        }

        public Menu ChangeMenu(Menu menu)
        {
            return bllMenu.SaveMenu(menu);
        }

        public Menu DisableMenu(Menu menu)
        {
            return bllMenu.DisableMenu(menu);
        }

        public Menu EnableMenu(Menu menu)
        {
            return bllMenu.EnableMenu(menu);
        }

        public void RemoveMenu(Menu menu)
        {
            bllMenu.RemoveMenu(menu);
        }
    }
}
