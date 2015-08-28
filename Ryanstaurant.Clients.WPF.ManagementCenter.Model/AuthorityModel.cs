using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model.UMSProxy;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class AuthorityModel
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

        public int KeyCode { get; set; }



        public void Add()
        {
            var arrAuthorities = new Authority[1];
            arrAuthorities[0] = new Authority
            {
                Description = Description,
                ID = ID,
                Name = Name,
                KeyCode = KeyCode
            };

            var results = ServiceClient.AddAuthorities(arrAuthorities);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Refresh()
        {
            var arrAuthorities = new Authority[1];
            arrAuthorities[0] = new Authority
            {
                ID = ID,
                Name = Name
            };

            var results = ServiceClient.GetAuthorities(arrAuthorities);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            var authReturn = results.ResultObject.FirstOrDefault();


            if (authReturn == null)
            {
                throw new Exception(
                    ID == -1 ? "没有找到名称为[" + Name + "]对应的权限" : "没有找到ID为[" + ID + "]对应的人员");
            }

            ID = authReturn.ID;
            Description = authReturn.Description;
            Name = authReturn.Name;
            KeyCode = authReturn.KeyCode;

        }



        public void Modify()
        {
            var arrAuthority = new Authority[1];
            arrAuthority[0] = new Authority
            {
                ID = ID,
                Name = Name,
                Description=Description,
                KeyCode = KeyCode
            };


            var results = ServiceClient.ModifyAuthorities(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete()
        {
            var arrAuthority = new Authority[1];
            arrAuthority[0] = new Authority
            {
                Description = Description,
                ID = ID,
                Name = Name,
                KeyCode = KeyCode
            };



            var results = ServiceClient.DeleteAuthorities(arrAuthority);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Reset();
        }


        public void Reset()
        {
            _id = -1;
            Name = string.Empty;
            Description = string.Empty;
            KeyCode = 0;
        }





    }
}
