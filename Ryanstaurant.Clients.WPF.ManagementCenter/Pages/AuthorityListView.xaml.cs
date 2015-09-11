using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// AuthorityListView.xaml 的交互逻辑
    /// </summary>
    public partial class AuthorityListView
    {

        private AuthorityListViewModel _viewModel;

        public AuthorityListView()
        {
            InitializeComponent();
            RefreshViewModel();
        }


        private void RefreshViewModel()
        {
            _viewModel = new AuthorityListViewModel();
            _viewModel.ShowAuthorityDetailHandler += vm_ShowDetailHandler;
            _viewModel.ShowMessageBoxHandler += ViewBase.ShowMessageBox;
            DataContext = _viewModel;
        }



        void vm_ShowDetailHandler(AuthorityViewModel auth)
        {
            if (auth == null)
                auth = new AuthorityViewModel();
            var view = new AuthorityView(auth);
            var btnOk = view.OkButton;
            btnOk.Content = "保存";
            btnOk.Command = auth.SaveCommand;
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
