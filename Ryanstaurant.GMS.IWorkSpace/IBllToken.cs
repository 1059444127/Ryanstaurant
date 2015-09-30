using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Ryanstaurant.GMS.IWorkSpace
{
    public interface IBllToken
    {

        string Login(string userName, string password);



    }
}
