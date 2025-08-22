using API.Controllers;
using API.Models;
using API.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace API.Tests
{
    public class UsersControllerTest
    {
        Mock<ILogger<UsersController>> loggerService;

        List<User> users;

        public UsersControllerTest()
        {
            loggerService = new Mock<ILogger<UsersController>>();

            users = new List<User> {
                new User
                {
                    Id = 1,
                    Username = "admin",
                },
                new User
                {
                    Id = 2,
                    Username = "ricardo",
                },
            };
        }

        [Fact]
        public async Task TestGetAll()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(Task.FromResult((IEnumerable<User>)users));

            var service = new UsersController(loggerService.Object, mockRepository.Object);
            var usersFound = await service.Index();

            Assert.Equal(usersFound, users);
        }

        [Fact]
        public async Task TestGetById()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).Returns(Task.FromResult((User?)users[1]));

            var service = new UsersController(loggerService.Object, mockRepository.Object);
            var userFound = await service.GetById(1);

            Assert.Equal(userFound.Value, (User?)users[1]);
        }

        [Fact]
        public async Task TestRegister()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetById(1)).Returns(Task.FromResult((User?)users[1]));

            var service = new UsersController(loggerService.Object, mockRepository.Object);
            var newUser = await service.Register(new RegisterUser { Username = "admin", Password = "1234" });

            Assert.Equal(newUser.Value, (User?)users[1]);
        }
    }
}
