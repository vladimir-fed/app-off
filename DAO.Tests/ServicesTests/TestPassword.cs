
using System;
using System.Collections.Generic;
using System.Linq;
using DAO.DTO;
using DAO.Models;
using DAO.Repositories;
using DAO.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace DAO.Tests.ServicesTests
{
    public class TestPassword : IClassFixture<DIFixture>
    {
        private readonly DIFixture _dIFixture;

        public TestPassword(DIFixture fixture)
        {
            _dIFixture = fixture;
        }

        [Fact]
        public void TestLogin()
        {
            var users = new List<User>();
            var mockRepository = new Mock<IUsersRepository>();
            mockRepository.Setup(m => m.Create(It.IsAny<User>())).Callback<User>(u => users.Add(u));
            mockRepository.Setup(m => m.GetAll()).Returns(() => users.AsQueryable());
            mockRepository.Setup(m => m.Save()).Callback(() => { });

            _dIFixture.DIContainer.Configure(c => c.ExportInstance(mockRepository.Object).As<IUsersRepository>());

            var service = _dIFixture.DIContainer.Locate<UsersService>();
            service.Create(new SignUpDto
            {
                Name = "Name",
                Password = "Password",
                UserName = "UserName"
            });
            
            service.Login(new LoginDTO {Password = "Password", UserName = "UserName"}).Should().NotBeNull();
        }
    }
}
