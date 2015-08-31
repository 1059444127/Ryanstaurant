using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FirstFloor.ModernUI.Presentation;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel
{
    public class EmployeeListViewModel
    {

        public ObservableCollection<EmployeeViewModel> EmployeeList { get; set; }



        public EmployeeListViewModel()
        {
            EmployeeList = new ObservableCollection<EmployeeViewModel>();
            var emplist = new EmployeeListModel().Collection.Select(em => new EmployeeViewModel(em.ID,em.Name)
            {
                Description = em.Description,
                LoginName = em.LoginName,
                Password = em.Password
            }).ToList();


            //合并权限
            foreach (var employee in emplist)
            {
                EmployeeList.Add(employee);
            }
        }



        public ICommand testCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    var newEmp = new EmployeeViewModel
                    {
                        Employee = new EmployeeModel
                        {
                            Description = Guid.NewGuid().ToString(),
                            LoginName = "testtest" + DateTime.Now.ToString("yyyy-M-d dddd"),
                            Name = "a" + (new Random()).Next().ToString("D"),
                            Password = "******"
                        }
                    };


                    newEmp.Employee.Add();
                    EmployeeList.Add(newEmp);


                });
            }
        }


    }
}
