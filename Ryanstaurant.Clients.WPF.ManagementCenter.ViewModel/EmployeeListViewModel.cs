using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;
using RelayCommand = FirstFloor.ModernUI.Presentation.RelayCommand;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class EmployeeListViewModel:ViewModelBase
    {


        public ObservableCollection<EmployeeViewModel> EmployeeList
        {
            get; set;
        }


        private EmployeeViewModel _currentEmployee;

        public EmployeeViewModel CurrentEmployee
        {
            get
            {
                return _currentEmployee;
            }
            set
            {
                _currentEmployee = value;
                if (_currentEmployee == null) return;
                SetCurrentRoleCheckes();
                SetCurrentAuthorityCheckes();
            }
        }



        public ObservableCollection<RoleViewModel> RoleList { get; set; }



        public ObservableCollection<AuthorityViewModel> AuthorityList { get; set; }


        public event ShowDetail<EmployeeViewModel> ShowEmployeeDetailHandler;




        public EmployeeListViewModel()
        {
            Refresh();
        }


        private void Refresh()
        {
            //获取所有相关数据
            var empViewModels = (from e in new EmployeeModel().GetAllEmmployees()
                select new EmployeeViewModel
                {
                    Employee = e
                }).ToList();
            var roleViewModels = (from r in new RoleModel().GetAllRoles()
                select new RoleViewModel
                {
                    Role = r
                }).ToList();
            var authViewModels = (from a in new AuthorityModel().GetAllAuthorities()
                select new AuthorityViewModel
                {
                    Authority = a
                }).ToList();

            //设置到基本属性
            if (EmployeeList==null)
            EmployeeList = new ObservableCollection<EmployeeViewModel>(empViewModels);
            else
            {
                EmployeeList.Clear();
                foreach (var employeeViewModel in empViewModels)
                {
                    EmployeeList.Add(employeeViewModel);
                }
            }

            if (RoleList==null)
            RoleList = new ObservableCollection<RoleViewModel>(roleViewModels);
            else
            {
                RoleList.Clear();
                foreach (var roleViewModel in roleViewModels)
                {
                    RoleList.Add(roleViewModel);
                }
            }
            if (AuthorityList==null)
            AuthorityList = new ObservableCollection<AuthorityViewModel>(authViewModels);
            else
            {
                AuthorityList.Clear();
                foreach (var authViewModel in authViewModels)
                {
                    AuthorityList.Add(authViewModel);
                }
            }


        }



        private void SetCurrentRoleCheckes()
        {
            //勾选所有权限
            foreach (var roleViewModel in RoleList)
            {
                var existRole = (from r in CurrentEmployee.RoleList
                                 where r.ID == roleViewModel.ID
                                 select r).ToList();

                roleViewModel.IsChecked = existRole.Any();
            }
        }



        private void SetCurrentAuthorityCheckes()
        {

            //勾选所有需要的权限
            foreach (var authorityViewModel in AuthorityList)
            {
                authorityViewModel.IsChecked = (authorityViewModel.ID & CurrentEmployee.Employee.Authority) == authorityViewModel.ID;
            }
        }



        public ICommand RoleSelectChangeCommand
        {
            get
            {
                return new RelayCommand(roleid =>
                {
                    var selectedRole = (from r in RoleList where r.ID == (int) roleid select r).FirstOrDefault();
                    var employeeRole = (from r in CurrentEmployee.RoleList where r.ID == (int)roleid select r).FirstOrDefault();

                    if (selectedRole == null) return;

                    if (selectedRole.IsChecked && employeeRole == null)
                    {
                        employeeRole = selectedRole;
                        CurrentEmployee.AddRole(employeeRole);
                    }
                    else if (!selectedRole.IsChecked && employeeRole != null)
                    {
                        CurrentEmployee.DeleteRole(employeeRole);
                    }

                    SetCurrentAuthorityCheckes();
                });
            }
        }

        public ICommand AuthSelectChangeCommand
        {
            get
            {
                return new RelayCommand(au =>
                {
                    long authid;

                    if (!long.TryParse(au.ToString(), out authid))
                        return;


                    var selectedAuth = (from a in AuthorityList where a.ID == authid select a).FirstOrDefault();

                    if (selectedAuth == null) return;

                    if (selectedAuth.IsChecked)
                    {
                        CurrentEmployee.AddAuthority(authid);
                    }
                    else
                    {
                        CurrentEmployee.DeleteAuthority(authid);
                    }

                    SetCurrentAuthorityCheckes();
                });
            }
        }


        /// <summary>
        /// 显示窗体命令
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    switch (int.Parse(p.ToString()))
                    {
                        case 0:
                            CurrentEmployee = null;
                            break;
                        case 1:

                            if (CurrentEmployee != null)
                                CurrentEmployee.Operation = OperationType.Modify;
                            break;
                    }

                    if (ShowEmployeeDetailHandler != null)
                        ShowEmployeeDetailHandler(CurrentEmployee);

                });
            }
        }



        public ICommand DeleteEmployeeCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    if (MessageBoxResult.Cancel ==
                        ShowMessageBoxHandler("确定删除当前人员信息？", "注意", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk))
                        return;

                    CurrentEmployee.DeleteEmployee();
                    if (ShowMessageBoxHandler != null)
                    {
                        ShowMessageBoxHandler("删除成功", "操作完成", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    Refresh();
                });
            }
        }


        public event ShowMessageBox ShowMessageBoxHandler;





    }
}
