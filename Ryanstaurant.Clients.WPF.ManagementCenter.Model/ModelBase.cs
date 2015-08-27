using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Model
{
    public class ModelBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged!=null)
                PropertyChanged(this,new PropertyChangedEventArgs(name));

        }


    }
}
