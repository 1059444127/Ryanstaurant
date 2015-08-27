using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FirstFloor.ModernUI.Windows;
using Ryanstaurant.Clients.ManagementCenter.Proxy.UMS;
using Ryanstaurant.Clients.ManagementCenter.ViewModels.UMS;

namespace Ryanstaurant.Clients.ManagementCenter.Views.UMS
{
    /// <summary>
    /// Employee.xaml 的交互逻辑
    /// </summary>
    public partial class Employee:IContent
    {

        public ObservableCollection<EmployeeViewModel> EmployeeViewModels { get; set; }

        public Employee()
        {
            InitializeComponent();
        }



        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs e)
        {
            




            base.DataContext = this;
        }

        public void OnNavigatingFrom(FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}
