using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class EmployeeListViewModel
    {

        public ObservableCollection<EmployeeViewModel> EmployeeList { get; set; }



        public void LoadAll()
        {
            if (EmployeeList == null)
                EmployeeList = new ObservableCollection<EmployeeViewModel>();


            var employees = new EmployeeViewModel().GetAllEmployeeViewModels();
            foreach (var employeeModel in employees)
            {
                EmployeeList.Add(employeeModel);
            }
        }




        public ICommand LoadCommand
        {
            get
            {
                return new RelayCommand(o => LoadAll());
            }
        }

    }
}
