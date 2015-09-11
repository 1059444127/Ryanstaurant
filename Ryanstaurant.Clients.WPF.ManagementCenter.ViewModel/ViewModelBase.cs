using System.Windows;
using System.Windows.Media;
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


        public OperationType Operation
        {
            get;
            set;
        }


        public delegate MessageBoxResult ShowMessageBox(string msg, string title, MessageBoxButton button, MessageBoxImage img);

        public delegate void ShowDetail<in T>(T main);



        public bool IsModify
        {
            get
            {
                return Operation == OperationType.Modify;
            }
        }


        private Brush _forColor = new SolidColorBrush(Color.FromRgb(0x00, 0x50, 0xef));

        public Brush ForeColor
        {
            get
            {
                return _forColor;
            }
            set
            {
                _forColor = value;
                RaisePropertyChanged("ForeColor");
            }
        }

        private string _information = string.Empty;

        public string Information
        {
            get { return _information; }
            set { _information = value; RaisePropertyChanged("Information"); }

        }

    }
}
