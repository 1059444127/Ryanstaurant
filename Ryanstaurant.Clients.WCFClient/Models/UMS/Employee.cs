using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ryanstaurant.Clients.WCFClient.Models.UMS
{
    public class Employee
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public List<Authority> AuthorityList { get; set; }

        public List<Role> RoleList { get; set; } 



    }
}
