using System;
using System.Windows;
using Ryanstaurant.Clients.WCFClient.ViewModels.UMS;

namespace Ryanstaurant.Clients.WCFClient.Views.UMS
{
    /// <summary>
    /// EmployeeView.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeView
    {
        private readonly EmployeeViewModel _viewModel;

        public EmployeeView()
        {
            InitializeComponent();
            _viewModel = (EmployeeViewModel)base.DataContext;
        }

    }
}
