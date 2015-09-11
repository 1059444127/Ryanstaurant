using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// EmployeeListView.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeListView
    {

        private EmployeeListViewModel _viewModel;

        public EmployeeListView()
        {
            InitializeComponent();
            RefreshViewModel();
        }


        void vm_ShowEmployeeDetailHandler(EmployeeViewModel employee)
        {
            if (employee == null)
                employee = new EmployeeViewModel();
            var view = new EmployeeView(employee);
            var btnOk = view.OkButton;
            btnOk.Content = "保存";
            btnOk.Command = employee.SaveCommand;
            view.CloseButton.Content = "关闭";
            view.Buttons = new[]
            {
                view.OkButton,view.CloseButton
            };
            view.ShowDialog();
            RefreshViewModel();
        }


        private void RefreshViewModel()
        {
            _viewModel = new EmployeeListViewModel();
            _viewModel.ShowEmployeeDetailHandler += vm_ShowEmployeeDetailHandler;
            _viewModel.ShowMessageBoxHandler += ViewBase.ShowMessageBox;
            DataContext = _viewModel;
        }


        


    }
}
