using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories;

using Moq;
using Moq.EntityFrameworkCore;

namespace TestPettsStore
{
    public class TestUserRepository
    {
        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUserByID()
        {
            var user = new User { Id = 1, Firstname = "Shira", Password = "111", Username = "shst", Lastname = "Stern" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.getUserById(user.Id);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            var user = new User { Id = 1, Firstname = "Shira", Password = "111", Username = "shiraSt", Lastname = "Stern" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.getAllUsers();

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task CreateUser_AddsUser()
        {
            var user = new User { Id = 1, Firstname = "Shira", Password = "111", Username = "shiraSt", Lastname = "Stern" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var users = new List<User>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            await userRepository.addUser(user);

            mockContext.Verify(x => x.Users.AddAsync(user, It.IsAny<CancellationToken>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_UpdatesExistingUser()
        {
            var user1 = new User { Id = 1, Firstname = "Shira", Password = "111", Username = "shst", Lastname = "Stern" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var users = new List<User> { user1 };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            var updatedUser = new User { Id = 1, Firstname = "NewName", Password = "111", Username = "shst", Lastname = "Stern" };
            await userRepository.updateUser(user1.Id, updatedUser);

            mockContext.Verify(x => x.Users.Update(updatedUser), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task login_validUser()
        {
            var user = new User { Id = 1, Firstname = "Shira", Password = "111", Username = "shst", Lastname = "Stern" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var loginUser = new User { Password = "111", Username = "shst"};

            var result = await userRepository.login(loginUser);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task login_NotvalidUser()
        {
            var user = new User { Id = 1, Firstname = "Shira", Password = "111", Username = "shst", Lastname = "Stern" };
            var mockContext = new Mock<PettsStore_DataBaseContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserRepository(mockContext.Object);

            var loginUser = new User { Password = "111", Username = "shsth" };

            var result = await userRepository.login(loginUser);

            Assert.Null(result);
        }
    }
}
