using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Repositories;
using Services;
using DTOs;
using Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
namespace TestPettsStore
{
    public class TestUserService
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        public TestUserService()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task AddUser_WeakPassword_ThrowsException()
        {
            var weakUser = new UserRegisterDTO("shira", "stern", "123", "shiraStern1");

            var serviceMock = new Mock<UserService>(_userRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object) { CallBase = true };
            serviceMock.Setup(s => s.GetPassStrength("123")).Returns(false);

            await Assert.ThrowsAsync<ArgumentException>(() => serviceMock.Object.addUser(weakUser));
        }

        [Fact]
        public async Task AddUser_StrongPassword_ReturnsUserDTO()
        {
            var registerDto = new UserRegisterDTO("shira", "stern", "Strong123!", "shiraStern1");
            var user = new User { Id = 1,Firstname="shira", Lastname="stern", Username= "shiraStern1" };
            var userDTO = new UserDTO(1,"shira", "stern", "shiraStern1");

            _mapperMock.Setup(m => m.Map<UserRegisterDTO, User>(registerDto)).Returns(user);
            _userRepositoryMock.Setup(r => r.addUser(user)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<User, UserDTO>(user)).Returns(userDTO);

            var serviceMock = new Mock<UserService>(_userRepositoryMock.Object, _mapperMock.Object, _loggerMock.Object) { CallBase = true };
            serviceMock.Setup(s => s.GetPassStrength("Strong123!")).Returns(true);

            var result = await serviceMock.Object.addUser(registerDto);

            Assert.NotNull(result);
            Assert.Equal(userDTO.Id, result.Id);
            Assert.Equal(userDTO.FirstName, result.FirstName);
            Assert.Equal(userDTO.LastName, result.LastName);
            Assert.Equal(userDTO.UserName, result.UserName);
        }
    }
}
