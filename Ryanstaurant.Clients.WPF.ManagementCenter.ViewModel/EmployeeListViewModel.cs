using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class EmployeeListViewModel
    {
        #region 属性
        /// <summary>
        /// 人员基本信息
        /// </summary>
        public ObservableCollection<EmployeeViewModel> EmployeeList { get; set; }

        /// <summary>
        /// 当前人员角色列表
        /// </summary>
        public ObservableCollection<RoleViewModel> CurrentRoleList { get; set; }

        /// <summary>
        /// 当前人员权限列表
        /// </summary>
        public ObservableCollection<AuthorityViewModel> CurrentAuthorityList { get; set; }

        /// <summary>
        /// 当前人员
        /// </summary>
        public EmployeeViewModel CurrentEmployee { get; set; }


        /// <summary>
        /// 所有角色列表
        /// </summary>
        public List<RoleViewModel> AllRoleList { get; set; }

        /// <summary>
        /// 所有权限列表
        /// </summary>
        public List<AuthorityViewModel> AllAuthorityList { get; set; }

        #endregion


        private List<RoleAuthModel> _roleAuthList; 


        public EmployeeListViewModel()
        {
            EmployeeList = new ObservableCollection<EmployeeViewModel>();
            var emplist = new EmployeeListModel().Collection.Select(em => new EmployeeViewModel(em.ID,em.Name)
            {
                Description = em.Description,
                LoginName = em.LoginName,
                Password = em.Password
            }).ToList();



            //基本信息
            foreach (var employee in emplist)
            {
                EmployeeList.Add(employee);
            }


            CurrentRoleList = new ObservableCollection<RoleViewModel>();
            CurrentAuthorityList = new ObservableCollection<AuthorityViewModel>();


            var allroles = new RoleListModel().GetAllRoles();
            AllRoleList = new List<RoleViewModel>();
            foreach (var roleModel in allroles)
            {
                AllRoleList.Add(new RoleViewModel(roleModel.ID, roleModel.Name)
                {
                    IsChecked = (from c in CurrentRoleList where c.ID == roleModel.ID select c).Any(),
                    Description = roleModel.Description
                });
            }

            AllAuthorityList = new List<AuthorityViewModel>();
            var authList = new AuthorityListModel().GetAllAuthorities();
            foreach (var authModel in authList)
            {
                AllAuthorityList.Add(new AuthorityViewModel(authModel.ID, authModel.Name)
                {
                    IsChecked = (from c in CurrentAuthorityList where c.ID == authModel.ID select c).Any(),
                    Description = authModel.Description
                });
            }

            _roleAuthList = new RoleAuthListModel().Collection;

        }


        #region COMMAND
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

        public ICommand RoleListChanged
        {
            get
            {
                return new RelayCommand(rl =>
                {

                    CurrentRoleList.Clear();
                    //从关联关系挂载出角色列表
                    var roleIdList = (from e in CurrentEmployee.EmpRoleList select e.RoleId).ToList();

                    var roleList = new RoleListModel().GetRoles(roleIdList);
                    foreach (var roleModel in roleList)
                    {
                        CurrentRoleList.Add(new RoleViewModel(roleModel.ID, roleModel.Name)
                        {
                            Description = roleModel.Description
                        });
                    }


                    foreach (var roleViewModel in AllRoleList)
                    {
                        roleViewModel.IsChecked = roleIdList.Contains(roleViewModel.ID);
                    }


                });
            }
        }

        public ICommand AuthorityChanged
        {
            get
            {
                return new RelayCommand(ac =>
                {

                    CurrentAuthorityList.Clear();
                    //从关联关系挂载出权限列表
                    var authIdList = (from e in CurrentEmployee.EmpAuthorityList select e.AuthId).ToList();
                    var authList = new AuthorityListModel().GetAuthorities(authIdList);

                    foreach (var authModel in authList)
                    {
                        CurrentAuthorityList.Add(new AuthorityViewModel(authModel.ID, authModel.Name)
                        {
                            Description = authModel.Description
                        });
                    }

                    foreach (var authViewModel in AllAuthorityList)
                    {
                        authViewModel.IsChecked = authIdList.Contains(authViewModel.ID);
                    }

                });
            }
        }

        #endregion


    }
}
