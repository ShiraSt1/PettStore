using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;//_ userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO> AddUser(UserRegisterDTO user) // Change to PascalCase: AddUser
        {
            if(user.FirstName.Length<2 ||user.FirstName.Length>50)
            {
                _logger.LogWarning("FirstName {FirstName} is out of range", user.FirstName);
                throw new ArgumentException("FirstName is not valid.");
            }
            if (!IsValidEmail(user.UserName))
            {
                _logger.LogWarning("Invalid email: {Email}", user.UserName);
                throw new ArgumentException("Email is not valid.");
            }

            if (!GetPassStrength(user.Password))
            {
                throw new ArgumentException("Password is not strong enough. It must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
            }
            return _mapper.Map<User, UserDTO>(await userRepository.addUser(_mapper.Map<UserRegisterDTO, User>(user)));
        }
        public async Task<UserDTO> UpdateUser(int id, UserRegisterDTO userUpdate) // Change to PascalCase: UpdateUser
        {
            if (userUpdate.FirstName.Length < 2 || userUpdate.FirstName.Length > 50)
            {
                _logger.LogWarning("FirstName {FirstName} is out of range", userUpdate.FirstName);
                throw new ArgumentException("FirstName is not valid.");
            }
            if (!IsValidEmail(userUpdate.UserName))
            {
                _logger.LogWarning("Invalid email: {Email}", userUpdate.UserName);
                throw new ArgumentException("Email is not valid.");
            }

            if (!GetPassStrength(userUpdate.Password))
            {
                throw new ArgumentException("Password is not strong enough. It must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
            }
            return _mapper.Map<User, UserDTO>(await userRepository.updateUser(id, _mapper.Map<UserRegisterDTO, User>(userUpdate)));
        }

        public async Task<UserDTO> login(UserLoginDTO newUser)
        {
            User user = await userRepository.login(_mapper.Map<UserLoginDTO, User>(newUser));
            if (user == null)
            {
                _logger.LogWarning("Login failed for username: {UserName}. Invalid credentials or user not found.", newUser.UserName);
                return null;
            }

            _logger.LogInformation("User {UserName} logged in successfully", user.Username);
            return _mapper.Map<User, UserDTO>(user);
        }

        public Boolean GetPassStrength(string password)
        {
            var result= Zxcvbn.Core.EvaluatePassword(password).Score;
            return result >= 3;
        }

        public async Task<UserDTO> getUserById(int id)//GetUserById
        {
            return _mapper.Map<User, UserDTO>(await userRepository.getUserById(id));
        }
        public async Task<List<UserDTO>> getAllUsers()//GetAllUsers
        {
            return _mapper.Map<List<User>, List<UserDTO>>(await userRepository.getAllUsers());
        }
        private bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
