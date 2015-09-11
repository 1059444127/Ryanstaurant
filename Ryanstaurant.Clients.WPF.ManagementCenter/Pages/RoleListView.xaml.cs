using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// RoleListView.xaml 的交互逻辑
    /// </summary>
    public partial class RoleListView
    {
        private RoleListViewModel _viewModel;

        public RoleListView()
        {
            InitializeComponent();
            RefreshViewModel();
        }

        private void RefreshViewModel()
        {
            _viewModel = new RoleListViewModel();
            _viewModel.ShowRoleDetailHandler += vm_ShowRoleDetailHandler;
            _viewModel.ShowMessageBoxHandler += ViewBase.ShowMessageBox;
            DataContext = _viewModel;
        }



        void vm_ShowRoleDetailHandler(RoleViewModel role)
        {
            if (role == null)
                role = new RoleViewModel();
            var view = new RoleView(role);
            var btnOk = view.OkButton;
            btnOk.Content = "保存";
            btnOk.Command = role.SaveCommand;
            view.CloseButton.Content = "关闭";
            view.Buttons = new[]
            {
                view.OkButton,view.CloseButton
            };
            view.ShowDialog();
            RefreshViewModel();
        }






    }
}
