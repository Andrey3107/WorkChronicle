namespace WebAPI.Test.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using CodeFirst.Interfaces;
    using CodeFirst.Interfaces.WorkChronicle;
    using CodeFirst.Models.Entities;

    using Microsoft.Extensions.Configuration;

    using Moq;

    using NUnit.Framework;

    using WebAPI.Services;
    using WebAPI.Services.Interfaces;
    
    public class UserServiceTests : BaseTestFixture
    {
        private IUserService userService;

        private Mock<IUnitOfWork> unitOfWork;

        private Mock<IUserRepository> userRepository;

        private Mock<IUserRoleRepository> userRoleRepository;

        private Mock<IConfiguration> configuration;

        [SetUp]
        public void Init()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            configuration = new Mock<IConfiguration>();

            MockTransaction(unitOfWork);

            SetupUserRepositoryMock();
            SetupUserRoleRepositoryMock();
            
            //userService = new UserService(unitOfWork.Object, configuration.Object);
        }

        [Test]
        public void IsUniqueUser_WhenSetExistingEmail_ReturnFalse()
        {
            Assert.Pass();
        }

        [Test]
        public void IsUniqueUser_WhenSetNotExistingEmail_ReturnTrue()
        {
            Assert.Pass();
        }

        private void SetupUserRepositoryMock()
        {
            userRepository = new Mock<IUserRepository>();

            var userResult = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "1",
                    FirstName = "1",
                    LastName = "1",
                    Password = "1",
                    Salt = "1",
                    PositionId = 1
                }
            };

            userRepository.Setup(x => x.GetAsQueryable(It.IsAny<bool>())).Returns(() => CreateGetAsQueryable(userResult.AsQueryable()));
        }

        private void SetupUserRoleRepositoryMock()
        {
            userRoleRepository = new Mock<IUserRoleRepository>();

            var userRoleResult = new List<UserRole>();

            userRoleRepository.Setup(x => x.GetAsQueryable(It.IsAny<bool>())).Returns(() => CreateGetAsQueryable(userRoleResult.AsQueryable()));
        }
    }
}
