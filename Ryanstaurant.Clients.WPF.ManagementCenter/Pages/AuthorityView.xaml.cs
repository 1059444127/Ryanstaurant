using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// AuthorityView.xaml 的交互逻辑
    /// </summary>
    public partial class AuthorityView
    {
        public AuthorityView(AuthorityViewModel authority)
        {
            InitializeComponent();
            Title = "权限信息";
            if (authority == null)
                authority = new AuthorityViewModel();
            authority.Close += Close;
            DataContext = authority;
        }
    }
}
