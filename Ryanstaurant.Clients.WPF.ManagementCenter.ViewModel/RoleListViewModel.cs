using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class RoleListViewModel:ViewModelBase
    {

        public ObservableCollection<RoleViewModel> RoleList
        { get; set; }



        private RoleViewModel _currentRole;

        public RoleViewModel CurrentRole
        {
            get
            {
                return _currentRole;
            }
            set
            {
                _currentRole = value;
                if (_currentRole == null) return;
                SetCurrentAuthorityCheckes();
            }

        }

        private void SetCurrentAuthorityCheckes()
        {
            //勾选所有需要的权限
            foreach (var authorityViewModel in AuthorityList)
            {
                authorityViewModel.IsChecked = (authorityViewModel.ID & CurrentRole.Authority) == authorityViewModel.ID;
            }
        }


        public ObservableCollection<AuthorityViewModel> AuthorityList { get; set; }



        public event ShowDetail<RoleViewModel> ShowRoleDetailHandler;



        public RoleListViewModel()
        {
            Refresh();
        }

        private void Refresh()
        {
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

            if (RoleList == null)
                RoleList = new ObservableCollection<RoleViewModel>(roleViewModels);
            else
            {
                RoleList.Clear();
                foreach (var employeeViewModel in roleViewModels)
                {
                    RoleList.Add(employeeViewModel);
                }
            }

            if (AuthorityList == null)
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



        public ICommand RoleSelectChangeCommand
        {
            get
            {
                return new RelayCommand(roleid => SetCurrentAuthorityCheckes());
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
                        CurrentRole.AddAuthority(authid);
                    }
                    else
                    {
                        CurrentRole.DeleteAuthority(authid);
                    }

                    SetCurrentAuthorityCheckes();
                });
            }
        }


        public ICommand ShowWindowCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    switch (int.Parse(p.ToString()))
                    {
                        case 0:
                            CurrentRole = null;
                            break;
                        case 1:

                            if (CurrentRole != null)
                                CurrentRole.Operation =OperationType.Modify;
                            break;
                    }

                    if (ShowRoleDetailHandler != null)
                        ShowRoleDetailHandler(CurrentRole);

                });
            }
        }



        public ICommand DeleteRoleCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    if (MessageBoxResult.Cancel ==
                        ShowMessageBoxHandler("确定删除当前人员信息？", "注意", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk))
                        return;

                    CurrentRole.DeleteEmployee();
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
