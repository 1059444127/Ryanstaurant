using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkSpace;
using BusinessModels;

namespace Models
{
    public class LoginPresenterModel
    {

        public string LoginID { get; set; }


        public string LoginName { get; set; }


        public string Password { get; set; }



        public string MgBoxMessage { get; set; }




        public string CheckUserPassword()
        {
            Employee emp = new Employee();

            BusinessLogic bl = new BusinessLogic();
            emp = bl.GetEmployee(LoginName.Replace("'", "").Replace("\\", "/").Trim());
            if (emp == null)
            {

                return "没有此用户名";
            }
            if (emp.Password != Password)
            {

                return "输入的密码错误";
            }


            return "登录成功";

        }
    }
}
