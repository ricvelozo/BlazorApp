using Api.Api;
using Api.Api.Auth;
using Api.Domain.Users;
using Api.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Api.Tests
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

            Assert.Equal(users, usersFound);
        }

        [Fact]
        public async Task TestGetById()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetById(2)).Returns(Task.FromResult((User?)users[1]));

            var service = new UsersController(loggerService.Object, mockRepository.Object);
            var userFound = (await service.GetById(2)).Result as OkObjectResult;

            Assert.Equal((User?)users[1], userFound.Value);
        }

        [Fact]
        public async Task TestCreate()
        {
            var credentials = new UserCredentialsDto { Username = "admin", Password = "1234" };

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Create(credentials)).Returns(Task.FromResult((User?)users[0]));

            var service = new UsersController(loggerService.Object, mockRepository.Object);
            var newUser = (await service.Create(credentials)).Result as OkObjectResult;

            Assert.Equal((User?)users[0], newUser.Value);
        }
    }
}
