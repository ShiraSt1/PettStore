using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPettsStore
{
    public class IntegrationTestUserRepository : IClassFixture<DatabaseFixture>
    {
        private readonly PettsStore_DataBaseContext _dbContext;
        private readonly UserRepository _userRepository;
        public IntegrationTestUserRepository(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var user = new User { Firstname = "Shira", Password = "111", Username = "shst", Lastname = "Stern" };
            var loginUser = new User { Password = "111", Username = "shst" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await _userRepository.login(loginUser);

            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task AddUser_ValidUser_ReturnsAddedUser()
        {
            var user = new User { Firstname = "Test", Password = "123", Username = "testUser", Lastname = "User" };

            var result = await _userRepository.addUser(user);

            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Firstname, result.Firstname);
        }


        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            var user = new User { Firstname = "Shira", Password = "111", Username = "shst", Lastname = "Stern" };

            await _userRepository.addUser(user);

            var loginUser = new User { Username = "shst", Password = "111" };
            var result = await _userRepository.login(loginUser);

            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }

        [Fact]
        public async Task UpdateUser_ValidId_ReturnsUpdatedUser()
        {
            var user = new User { Firstname = "OldName", Password = "111", Username = "updateUser", Lastname = "Old" };
            await _userRepository.addUser(user);

            var userUpdate = new User { Firstname = "NewName", Password = "111", Username = "updateUser", Lastname = "New" };
            var result = await _userRepository.updateUser(user.Id, userUpdate);

            Assert.NotNull(result);
            Assert.Equal("NewName", result.Firstname);
        }

        [Fact]
        public async Task GetUserById_ValidId_ReturnsUser()
        {
            var user = new User { Firstname = "Test", Password = "123", Username = "getIdUser", Lastname = "User" };
            await _userRepository.addUser(user);

            var result = await _userRepository.getUserById(user.Id);

            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsListOfUsers()
        {
            await _userRepository.addUser(new User { Firstname = "User1", Username = "user1", Password = "pass" });
            await _userRepository.addUser(new User { Firstname = "User2", Username = "user2", Password = "pass" });

            var result = await _userRepository.getAllUsers();

            Assert.NotNull(result);
            Assert.True(result.Count >= 2); // Ensure it returns all users added
        }

        [Fact]
        public async Task Login_InvalidUsername_ReturnsNull()
        {
            var loginuser = new User { Username = "unknownUser", Password = "password" };
            var result = await _userRepository.login(loginuser);

            Assert.Null(result);
        }
        [Fact]
        public async Task Login_InvalidPassword_ReturnsNull()
        {
            var user = new User { Firstname = "Ruth", Lastname = "Hermelin", Username = "ruthher1", Password = "8197" };
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var loginuser = new User { Username = "ruthher1", Password = "wrongPassword" };

            var result = await _userRepository.login(loginuser);

            Assert.Null(result);
        }
        [Fact]
        public async Task Login_EmptyCredentials_ReturnsNull()
        {
            var loginuser = new User { Username = "", Password = "" };
            var result = await _userRepository.login(loginuser);

            Assert.Null(result);
        }
    }
}