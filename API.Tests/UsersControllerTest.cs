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
                    Password = new byte[] { 0, 1, 2, 3, 4 },
                },
                new User
                {
                    Id = 2,
                    Username = "ricardo",
                    Password = new byte[] { 0, 1, 2, 3, 4 },
                },
            };
        }

        [Fact]
        public async Task TestGetAllUsers()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetAllUsers()).Returns(Task.FromResult((IEnumerable<User>)users));

            var service = new UsersController(loggerService.Object, mockRepository.Object);
            var usersFound = await service.Index();

            Assert.Equal(usersFound, users);
        }

        [Fact]
        public async Task TestGetUserById()
        {
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.GetUserById(1)).Returns(Task.FromResult((User?)users[1]));

            var service = new UsersController(loggerService.Object, mockRepository.Object);
            var userFound = await service.GetUserById(1);

            Assert.Equal(userFound.Value, (User?)users[1]);
        }
    }
}
