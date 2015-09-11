using System.Windows;
using MicroMvvm;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class ViewModelBase:ObservableObject
    {
        public enum OperationType
        {
            Add = 0,
            Modify = 1,
            Delete = 2
        }




        public delegate MessageBoxResult ShowMessageBox(string msg, string title, MessageBoxButton button, MessageBoxImage img);

        public delegate void ShowDetail<in T>(T main);

    }
}
