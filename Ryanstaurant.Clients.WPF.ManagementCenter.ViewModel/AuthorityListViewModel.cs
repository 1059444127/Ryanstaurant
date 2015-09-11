using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class AuthorityListViewModel : ViewModelBase
    {


        public ObservableCollection<AuthorityViewModel> AuthorityList { get; set; }


        public AuthorityViewModel CurrentAuthority { get; set; }


        public event ShowDetail<AuthorityViewModel> ShowAuthorityDetailHandler;


        public AuthorityListViewModel()
        {
            Refresh();
        }

        private void Refresh()
        {
            var authViewModels = (from a in new AuthorityModel().GetAllAuthorities()
                                  select new AuthorityViewModel
                                  {
                                      Authority = a
                                  }).ToList();

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

        public ICommand ShowWindowCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    switch (int.Parse(p.ToString()))
                    {
                        case 0:
                            CurrentAuthority = null;
                            break;
                        case 1:

                            if (CurrentAuthority != null)
                                CurrentAuthority.Operation = OperationType.Modify;
                            break;
                    }

                    if (ShowAuthorityDetailHandler != null)
                        ShowAuthorityDetailHandler(CurrentAuthority);

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
                        ShowMessageBoxHandler("当前操作将会修改所有相关此权限的人员以及角色信息\r\n确定删除当前权限信息？", "注意", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk))
                        return;

                    CurrentAuthority.DeleteAuthority();
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
