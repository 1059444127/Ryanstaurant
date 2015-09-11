using System.Windows;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Pages
{
    /// <summary>
    /// ViewBase.xaml 的交互逻辑
    /// </summary>
    public partial class ViewBase
    {
        public ViewBase()
        {
            InitializeComponent();
        }

        internal static MessageBoxResult ShowMessageBox(string msg, string title, MessageBoxButton button, MessageBoxImage img)
        {
            return MessageBox.Show(msg, title, button, img);
        }

    }
}
