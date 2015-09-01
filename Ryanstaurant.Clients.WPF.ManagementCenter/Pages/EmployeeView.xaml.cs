using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// EmployeeView.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        public EmployeeView(int id,string name)
        {
            InitializeComponent();
            base.DataContext = new EmployeeViewModel(id, name);
        }

        public EmployeeView()
        {
            InitializeComponent();
            base.DataContext = new EmployeeViewModel();
        }
    }
}
