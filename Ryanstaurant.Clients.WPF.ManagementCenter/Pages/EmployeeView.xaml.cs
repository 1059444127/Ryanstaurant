using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// EmployeeView.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeView
    {
        public EmployeeView(EmployeeViewModel employee)
        {
            InitializeComponent();
            Title = "员工信息";
            if (employee == null)
                employee = new EmployeeViewModel();
            employee.Close += Close;
            DataContext = employee;
        }



    }
}
