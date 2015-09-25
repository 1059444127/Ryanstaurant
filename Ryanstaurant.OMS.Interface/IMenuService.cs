using System.Collections.Generic;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Interface
{
    public interface IMenuService
    {
        IList<Menu> GetMenus();
        Menu AddMenu(Menu menu);
        Menu ChangeMenu(Menu menu);
        Menu DisableMenu(Menu menu);
        Menu EnableMenu(Menu menu);
        void RemoveMenu(Menu menu);

    }
}
