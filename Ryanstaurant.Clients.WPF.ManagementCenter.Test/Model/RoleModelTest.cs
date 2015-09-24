using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;
using Ryanstaurant.UMS.Client;
using Ryanstaurant.UMS.DataContract;
using Ryanstaurant.UMS.DataContract.Utility;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Test.Model
{
    [TestClass]
    public class RoleModelTest
    {
        private Mock<IUMSClient> serviceClientMock = new Mock<IUMSClient>();


        private List<Role> SimDB = new List<Role>
        {
            new Role
            {
                Authority = 3,
                ID = 1,
                Name = "Administrator",
                Description = "Administrator"
            },
            new Role
            {
                Authority = 1,
                ID = 4,
                Name = "Server",
                Description = "Server"
            },
            new Role
            {
                Authority = 2,
                ID = 7,
                Name = "Chef",
                Description = "Chef"
            }
        };


        private List<Role> _currentModels;



        [TestMethod]
        public void AddTest()
        {
            _currentModels = new List<Role>
            {
                new Role
                {
                    Description = "",
                    ID = 1,
                    Name = "Administrator",
                    Authority = 3,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Add,
                        State = ResultState.Success
                    }
                }
            };




            serviceClientMock.Setup(s => s.Execute(It.IsAny<List<ItemContent>>())).Returns(() =>
            {
                if (_currentModels == null || _currentModels.Count==0)
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
                    
                    resultModels.Add(new Role
                    {
                        Authority = currentModel.Authority,
                        ID = currentModel.ID,
                        Description = currentModel.Description,
                        Name = currentModel.Name,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Add,
                            State=ResultState.Success
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
                    var role =
                        (from r in SimDB where r.ID == currentModel.ID select r)
                            .FirstOrDefault();
                    if (role == null)
                        continue;

                    resultModels.Add(role);
                }

                return resultModels;

            });

            var rolemodel = new RoleModel(serviceClientMock.Object);

            rolemodel.Add();

            Assert.AreEqual(1, rolemodel.ID, "添加操作返回错误ID号");

        }

        [TestMethod]
        public void ModifyTest()
        {
            _currentModels = new List<Role>
            {
                new Role
                {
                    Description = "",
                    ID = 1,
                    Name = "Administrator",
                    Authority = 3,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Modify,
                        State = ResultState.Success
                    }
                }
            };



            serviceClientMock.Setup(s => s.Execute(It.IsAny<List<ItemContent>>())).Returns(()=>
            {
                if (_currentModels == null || _currentModels.Count==0)
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


                    existModel.Authority = currentModel.Authority;
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
                    var role = (from r in SimDB where r.ID==currentModel.ID select r).FirstOrDefault();
                    if (role == null)
                        continue;

                    resultModels.Add(role);
                }

                return resultModels;

            });


            var rolemodel = new RoleModel(serviceClientMock.Object);

            rolemodel.Modify();

            Assert.AreEqual(1, rolemodel.ID, "修改操作返回错误ID号");

        }

        [TestMethod]
        public void DeleteTest()
        {
            serviceClientMock.Setup(s => s.Execute(It.IsAny<List<ItemContent>>())).Returns(new List<ItemContent>
            {
                new Role
                {
                    Description = "",
                    ID = 1,
                    Name = "ADMINISTRATOR",
                    Authority = 3,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Delete,
                        State = ResultState.Success
                    }
                }
            });


            var rolemodel = new RoleModel(serviceClientMock.Object);

            rolemodel.Delete();

            Assert.AreEqual(-1, rolemodel.ID, "修改操作返回错误ID号");

        }


        [TestMethod]
        public void GetAllRolesTest()
        {
            serviceClientMock.Setup(s => s.GetAllRoles()).Returns(() =>
            {
                var re = new List<ItemContent>();
                foreach (var role in SimDB)
                {
                    re.Add(new Role
                    {
                        Authority=role.Authority,
                        Description = role.Description,
                        ID = role.ID,
                        Name = role.Name,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query,
                            State = ResultState.Success
                        }
                    });
                }
                return re;
            });

            var model = new RoleModel(serviceClientMock.Object);

            var result = model.GetAllRoles();

            Assert.AreEqual(SimDB.Count, result.Count, "查询所有记录返回错误的数据个数");
        }





    }
}
