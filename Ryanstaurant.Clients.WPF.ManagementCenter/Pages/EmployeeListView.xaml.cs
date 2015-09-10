using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Media;
using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// EmployeeListView.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeListView : UserControl
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
            view.Buttons = new Button[]
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
            _viewModel.ShowMessageBoxHandler += ShowMessageBox;
            DataContext = _viewModel;
        }




        private static MessageBoxResult ShowMessageBox(string msg, string title,MessageBoxButton button,MessageBoxImage img)
        {
            return MessageBox.Show(msg, title, button, img);
        }




    }
}
