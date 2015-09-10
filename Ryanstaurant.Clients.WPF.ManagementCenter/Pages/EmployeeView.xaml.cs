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
using FirstFloor.ModernUI.Windows.Controls;
using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// EmployeeView.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeView : ModernDialog
    {
        public EmployeeView(EmployeeViewModel employee)
        {
            InitializeComponent();
            this.Title = "员工信息";
            if (employee == null)
                employee = new EmployeeViewModel();
            employee.Close += Close;
            base.DataContext = employee;
        }



    }
}
