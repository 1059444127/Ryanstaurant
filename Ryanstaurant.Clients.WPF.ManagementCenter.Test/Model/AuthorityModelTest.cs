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
    public class AuthorityModelTest
    {
        private Mock<IUMSClient> serviceClientMock = new Mock<IUMSClient>();


        private List<Authority> SimDB = new List<Authority>
        {
            new Authority
            {
                ID = 1,
                Name = "PrintCheck",
                Description = "PrintCheck"
            },
            new Authority
            {
                ID = 2,
                Name = "PrintReport",
                Description = "PrintReport"
            }
        };


        private List<Authority> _currentModels;



        [TestMethod]
        public void AddTest()
        {
            _currentModels = new List<Authority>
            {
                new Authority
                {
                    Description = "test",
                    ID = 1,
                    Name = "test",
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

                    resultModels.Add(new Authority
                    {
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
                    var role =
                        (from r in SimDB where r.ID == currentModel.ID select r)
                            .FirstOrDefault();
                    if (role == null)
                        continue;

                    resultModels.Add(role);
                }

                return resultModels;

            });

            var model = new AuthorityModel(serviceClientMock.Object);

            model.Add();

            Assert.AreEqual(1, model.ID, "添加操作返回错误ID号");

        }

        [TestMethod]
        public void ModifyTest()
        {
            _currentModels = new List<Authority>
            {
                new Authority
                {
                    Description = "PrintCheck",
                    ID = 1,
                    Name = "PrintCheck",
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
                    var role = (from r in SimDB where r.ID == currentModel.ID select r).FirstOrDefault();
                    if (role == null)
                        continue;

                    resultModels.Add(role);
                }

                return resultModels;

            });


            var model = new AuthorityModel(serviceClientMock.Object);

            model.Modify();

            Assert.AreEqual(1, model.ID, "修改操作返回错误ID号");

        }

        [TestMethod]
        public void DeleteTest()
        {
            serviceClientMock.Setup(s => s.Execute(It.IsAny<List<ItemContent>>())).Returns(new List<ItemContent>
            {
                new Role
                {
                    Description = "PrintCheck",
                    ID = 1,
                    Name = "PrintCheck",
                    Authority = 3,
                    CommandInfo = new CommandInformation
                    {
                        Operation = RequestOperation.Delete,
                        State = ResultState.Success
                    }
                }
            });

            var model = new AuthorityModel(serviceClientMock.Object);

            model.Delete();

            Assert.AreEqual(-1, model.ID, "修改操作返回错误ID号");

        }


        [TestMethod]
        public void GetAllAuthorityTest()
        {
            serviceClientMock.Setup(s => s.GetAllAuthorities()).Returns(() =>
            {
                var re = new List<ItemContent>();
                foreach (var auth in SimDB)
                {
                    re.Add(new Authority
                    {
                        Description = auth.Description,
                        ID = auth.ID,
                        Name = auth.Name,
                        CommandInfo = new CommandInformation
                        {
                            Operation = RequestOperation.Query,
                            State = ResultState.Success
                        }
                    });
                }
                return re;
            });

            var model = new AuthorityModel(serviceClientMock.Object);

            var result = model.GetAllAuthorities();

            Assert.AreEqual(SimDB.Count, result.Count, "查询所有记录返回错误的数据个数");
        }



    }
}
