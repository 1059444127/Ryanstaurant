using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class EmployeeListViewModel
    {

        public ObservableCollection<EmployeeViewModel> EmployeeList { get; set; }

        protected EmpRoleListModel EmpRolesList = new EmpRoleListModel();
        protected EmpAuthListModel EmpAuthsList = new EmpAuthListModel();
        protected RoleAuthListModel RoleAuthsList = new RoleAuthListModel();
        protected RoleListModel RoleList = new RoleListModel();
        protected AuthorityListModel AuthorityList = new AuthorityListModel();





        public EmployeeListViewModel()
        {
            EmployeeList = new ObservableCollection<EmployeeViewModel>();
            var emplist = new EmployeeListModel().Collection.Select(em => new EmployeeViewModel()
            {
                Description = em.Description,
                ID = em.ID,
                LoginName = em.LoginName,
                Name = em.Name,
                Password = em.Password
            }).ToList();



            //合并权限
            foreach (var employee in emplist)
            {
                var currentEmp = employee;
                var roleList =
                    (from er in EmpRolesList.Collection where er.EmpId == currentEmp.ID select er.RoleId).ToList();

                var roleAuth =
                    (from ra in RoleAuthsList.Collection where roleList.Contains(ra.RoleId) select ra.AuthId).ToList();

                var empAuth =
                    (from ea in EmpAuthsList.Collection where ea.EmpId == currentEmp.ID select ea.AuthId).ToList();


                var authList = roleAuth.Concat(empAuth).ToList();

                var rolelist = RoleList.GetRoles(roleList);

                foreach (var role in rolelist)
                {
                    employee.RoleList.Add(new RoleViewModel
                    {
                        Description = role.Description,
                        ID = role.ID,
                        Name = role.Name
                    });
                }


                var authlist = AuthorityList.GetAuthorities(authList);

                foreach (var auth in authlist)
                {
                    employee.AuthorityList.Add(new AuthorityViewModel
                    {
                        Description = auth.Description,
                        ID = auth.ID,
                        Name = auth.Name,
                        KeyCode = auth.KeyCode
                    });
                }

                EmployeeList.Add(employee);
            }
        }



        public ICommand testCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    var newEmp = new EmployeeViewModel
                    {
                        Employee = new EmployeeModel
                        {
                            Description = Guid.NewGuid().ToString(),
                            LoginName = "testtest" + DateTime.Now.ToString("yyyy-M-d dddd"),
                            Name = "a" + (new Random()).Next().ToString("D"),
                            Password = "******"
                        }
                    };


                    newEmp.Employee.Add();
                    EmployeeList.Add(newEmp);


                });
            }
        }


    }
}
