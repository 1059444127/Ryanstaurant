using System.Collections.Generic;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.UMS.Client
{
    public interface IUMSClient
    {
        string SessionToken { get; }

        string ErrorMessage { get; }

        ResultState State { get;  }

        string InnerErrorMessage { get; }


        List<ItemContent> Execute(List<ItemContent> requestObjects);

        List<ItemContent> Query(List<ItemContent> requestObjects);


        List<ItemContent> GetAllEmployees();
        List<ItemContent> GetAllAuthorities();

        List<ItemContent> GetAllRoles();

        bool Login(string userName, string password);


    }
}
