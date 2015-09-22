﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ryanstaurant.Clients.WPF.ManagementCenter.Model;
using Ryanstaurant.Clients.WPF.ManagementCenter.ViewModel;

namespace Ryanstaurant.Clients.WPF.ManagementCenter.Test.ViewModel
{
    [TestClass]
    public class EmployeeViewModelTest
    {

        private Mock<EmployeeModel> modelMock = new Mock<EmployeeModel>();

        [TestMethod]
        public void RefreshTest()
        {
            modelMock.Object.Name = "TEST1";
            modelMock.Object.ID = 1;
            modelMock.Object.LoginName = "TEST1";
            modelMock.Object.Password = "TEST1";
            modelMock.Object.Description = "TEST1";
            modelMock.Object.EmpAuthority = 1;
            modelMock.Object.Roles.Add(new RoleModel
            {
                Authority = 2,
                Description = "testRole",
                ID = 1,
                Name = "a"
            });


            var vm = new EmployeeViewModel
            {
                Employee = modelMock.Object
            };


            Assert.AreEqual(modelMock.Object.ID,vm.ID);


        }








    }
}
