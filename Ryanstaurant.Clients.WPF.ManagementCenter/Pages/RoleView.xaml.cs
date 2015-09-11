using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// RoleView.xaml 的交互逻辑
    /// </summary>
    public partial class RoleView
    {
        public RoleView(RoleViewModel role)
        {
            InitializeComponent();
            Title = "角色信息";
            if (role == null)
                role = new RoleViewModel();
            role.Close += Close;
            DataContext = role;
        }
    }
}
