using System;
using System.Linq;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model.UMSProxy;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class EmployeeModel
    {
        private int _id = -1;
        private string _name = string.Empty;
        private string _loginName = string.Empty;
        private string _password = string.Empty;
        private string _description = string.Empty;
        protected readonly UMSServiceClient ServiceClient = new UMSServiceClient();



        #region 属性
        public int ID { get { return _id; } set { _id = value; } }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string LoginName
        {
            get
            {
                return _loginName;
            }
            set
            {
                _loginName = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }


        #endregion


        public void Add()
        {
            var arrEmployee = new Employee[1];
            arrEmployee[0] =new Employee
            {
                Description = Description,
                ID = ID,
                LoginName = LoginName,
                Name = Name,
                Password = Password
            };

            var results = ServiceClient.AddEmployees(arrEmployee);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }


        public void Refresh()
        {
            var arrEmployee = new Employee[1];
            arrEmployee[0] = new Employee
            {
                ID = ID,
                Name = Name
            };

            var results = ServiceClient.GetEmployees(arrEmployee);
            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            var empReturn = results.ResultObject.FirstOrDefault();


            if (empReturn == null)
            {
                throw new Exception(ID == -1 ? "没有找到名称为[" + Name + "]对应的人员" : "没有找到ID为[" + ID + "]对应的人员");
            }

            ID = empReturn.ID;
            this.Description = empReturn.Description;
            this.LoginName = empReturn.LoginName;
            this.Name = empReturn.Name;
            this.Password = empReturn.Password;
        }

    

        public void Modify()
        {
            var arrEmployee = new Employee[1];
            arrEmployee[0] = new Employee
            {
                Description = Description,
                ID = ID,
                LoginName = LoginName,
                Name = Name,
                Password = Password
            };



            var results = ServiceClient.ModifyEmployees(arrEmployee);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

            Refresh();
        }

        public void Delete()
        {
            var arrEmployee = new Employee[1];
            arrEmployee[0] = new Employee
            {
                Description = Description,
                ID = ID,
                LoginName = LoginName,
                Name = Name,
                Password = Password
            };



            var results = ServiceClient.DeleteEmployees(arrEmployee);

            if (results.State == ResultState.Fail)
                throw new Exception(results.ErrorMessage);

           Reset();
        }


        public void Reset()
        {
            _description = string.Empty;
            _id = -1;
            _loginName = string.Empty;
            _name = string.Empty;
            _password = string.Empty;
        }



    }
}
