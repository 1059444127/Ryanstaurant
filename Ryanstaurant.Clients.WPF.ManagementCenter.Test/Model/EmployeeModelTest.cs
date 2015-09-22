using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Test.Model
{
    [TestClass]
    public class EmployeeModelTest
    {
        private Mock<IUMSClient> serviceClientMock = new Mock<IUMSClient>();


        private List<Employee> SimDB = new List<Employee>
        {
            new Employee
            {
                ID = 1,
                Name = "RYAN",
                Description = "RYAN",
                EmpAuthority = 3,
                LoginName = "RYAN",
                Password = "123456",
                Roles = new List<Role>{new Role{Authority = 3,ID = 1,Name = "ADMINISTRATOR"}}
            }
        };


        private List<Employee> _currentModels;



        [TestMethod]
        public void AddTest()
        {
            _currentModels = new List<Employee>
            {
                new Employee
                {
                    Description = "",
                    ID = 1,
                    Name = "Administrator",
                    EmpAuthority = 3,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Add,
                        State = ResultState.Success
                    }
                }
            };




            serviceClientMock.Setup(s => s.Execute(It.IsAny<List<ItemContent>>())).Returns(() =>
            {
                if (_currentModels == null || _currentModels.Count == 0)
                    return null;

                var resultModels = new List<ItemContent>();

                foreach (var currentModel in _currentModels)
                {
                    var existModel = (from r in SimDB where r.Equals(currentModel) select r).FirstOrDefault();

                    if (existModel != null)
                    {
                        existModel.CommandInfo.Exception = "ID为" + existModel.ID + ",名称为:" + existModel.Name + "已经存在";
                        existModel.CommandInfo.State = ResultState.Fail;
                        continue;
                    }

                    resultModels.Add(new Employee
                    {
                        EmpAuthority = currentModel.EmpAuthority,
                        ID = currentModel.ID,
                        Description = currentModel.Description,
                        Name = currentModel.Name,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Add,
                            State = ResultState.Success
                        }
                    });
                }

                return resultModels;

            });



            serviceClientMock.Setup(s => s.Query(It.IsAny<List<ItemContent>>())).Returns(() =>
            {
                if (_currentModels == null)
                    return null;

                var resultModels = new List<ItemContent>();

                foreach (var currentModel in _currentModels)
                {
                    var employee =
                        (from r in SimDB where r.ID == currentModel.ID select r)
                            .FirstOrDefault();
                    if (employee == null)
                        continue;

                    resultModels.Add(employee);
                }

                return resultModels;

            });

            var model = new EmployeeModel { UmsClient = serviceClientMock.Object };

            model.Add();

            Assert.AreEqual(1, model.ID, "添加操作返回错误ID号");

        }

        [TestMethod]
        public void ModifyTest()
        {
            _currentModels = new List<Employee>
            {
                new Employee
                {
                    Description = "",
                    ID = 1,
                    Name = "Administrator",
                    EmpAuthority = 3,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Modify,
                        State = ResultState.Success
                    }
                }
            };



            serviceClientMock.Setup(s => s.Execute(It.IsAny<List<ItemContent>>())).Returns(() =>
            {
                if (_currentModels == null || _currentModels.Count == 0)
                    return null;


                foreach (var currentModel in _currentModels)
                {
                    var existModel = (from r in SimDB where r.Equals(currentModel) select r).FirstOrDefault();

                    if (existModel == null)
                    {
                        currentModel.CommandInfo.Exception = "ID为" + currentModel.ID + ",名称为:" + currentModel.Name + "不存在";
                        currentModel.CommandInfo.State = ResultState.Fail;
                        continue;
                    }


                    existModel.EmpAuthority = currentModel.EmpAuthority;
                    existModel.ID = currentModel.ID;
                    existModel.Description = currentModel.Description;
                    existModel.Name = currentModel.Name;
                    existModel.CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Modify,
                        State = ResultState.Success
                    };
                }

                return _currentModels.Cast<ItemContent>().ToList();
            });

            serviceClientMock.Setup(s => s.Query(It.IsAny<List<ItemContent>>())).Returns(() =>
            {
                if (_currentModels == null)
                    return null;

                var resultModels = new List<ItemContent>();

                foreach (var currentModel in _currentModels)
                {
                    var employee = (from r in SimDB where r.ID == currentModel.ID select r).FirstOrDefault();
                    if (employee == null)
                        continue;

                    resultModels.Add(employee);
                }

                return resultModels;

            });


            var model = new EmployeeModel { UmsClient = serviceClientMock.Object };

            model.Modify();

            Assert.AreEqual(1, model.ID, "修改操作返回错误ID号");

        }

        [TestMethod]
        public void DeleteTest()
        {
            serviceClientMock.Setup(s => s.Execute(It.IsAny<List<ItemContent>>())).Returns(new List<ItemContent>
            {
                new Employee
                {
                    Description = "",
                    ID = 1,
                    Name = "ADMINISTRATOR",
                    EmpAuthority = 3,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Delete,
                        State = ResultState.Success
                    }
                }
            });


            var model = new EmployeeModel { UmsClient = serviceClientMock.Object };

            model.Delete();

            Assert.AreEqual(-1, model.ID, "修改操作返回错误ID号");

        }


        [TestMethod]
        public void GetAllEmployeesTest()
        {
            serviceClientMock.Setup(s => s.GetAllEmployees()).Returns(() =>
            {
                var re = new List<ItemContent>();
                foreach (var employee in SimDB)
                {
                    re.Add(new Employee
                    {
                        EmpAuthority = employee.EmpAuthority,
                        Description = employee.Description,
                        ID = employee.ID,
                        Name = employee.Name,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query,
                            State = ResultState.Success
                        },
                        Roles = new List<Role>()
                        
                    });
                }
                return re;
            });

            var model = new EmployeeModel { UmsClient = serviceClientMock.Object };

            var result = model.GetAllEmmployees();

            Assert.AreEqual(SimDB.Count, result.Count, "查询所有记录返回错误的数据个数");
        }








    }
}
