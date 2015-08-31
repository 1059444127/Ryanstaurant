using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model.UMSProxy;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class RoleModel
    {
        protected readonly UMSServiceClient ServiceClient = new UMSServiceClient();

        private int _id = -1;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name { get; set; }

        public string Description { get; set; }




        public void Add()
        {
            var arrRoles = new Role[1];
            arrRoles[0] = new Role
            {
                Description = Description,
                ID = ID,
                Name = Name
            };

            var results = ServiceClient.AddRoles(arrRoles);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Refresh()
        {
            var arrRoles = new Role[1];
            arrRoles[0] = new Role
            {
                ID = ID,
                Name = Name
            };

            var results = ServiceClient.GetRoles(arrRoles);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            var authReturn = results.ResultObject.FirstOrDefault();


            if (authReturn == null)
            {
                throw new Exception(
                    ID == -1 ? "没有找到名称为[" + Name + "]对应的角色" : "没有找到ID为[" + ID + "]对应的角色");
            }

            ID = authReturn.ID;
            Description = authReturn.Description;
            Name = authReturn.Name;

        }



        public void Modify()
        {
            var arrRole = new Role[1];
            arrRole[0] = new Role
            {
                ID = ID,
                Name = Name,
                Description = Description
            };


            var results = ServiceClient.ModifyRoles(arrRole);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete()
        {
            var arrRole = new Role[1];
            arrRole[0] = new Role
            {
                Description = Description,
                ID = ID,
                Name = Name
            };



            var results = ServiceClient.DeleteRoles(arrRole);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Reset();
        }


        public void Reset()
        {
            _id = -1;
            Name = string.Empty;
            Description = string.Empty;
        }









    }
}
