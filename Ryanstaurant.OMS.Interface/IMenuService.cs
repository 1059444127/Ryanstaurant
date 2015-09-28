using System.Collections.Generic;
using System.ServiceModel;
using Ryanstaurant.OMS.DataContract;

namespace Ryanstaurant.OMS.Interface
{
    [ServiceContract]
    public interface IMenuService
    {
        [OperationContract]
        IList<Menu> GetMenus();
        [OperationContract]
        Menu AddMenu(Menu menu);
        [OperationContract]
        Menu ChangeMenu(Menu menu);
        [OperationContract]
        Menu DisableMenu(Menu menu);
        [OperationContract]
        Menu EnableMenu(Menu menu);
        [OperationContract]
        void RemoveMenu(Menu menu);

    }
}
