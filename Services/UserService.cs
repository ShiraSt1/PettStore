using AutoMapper;
using DTOs;
using Entities;
using Repositories;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO> addUser(UserRegisterDTO user)
        {
            if (!GetPassStrength(user.Password))
            {
                throw new ArgumentException("Password is not strong enough. It must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
            }
            return _mapper.Map<User, UserDTO>(await userRepository.addUser(_mapper.Map<UserRegisterDTO, User>(user)));
        }

        public async Task<UserDTO> updateUser(int id, UserRegisterDTO userUpdate)
        {
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

        public async Task<UserDTO> getUserById(int id)
        {
            return _mapper.Map<User, UserDTO>(await userRepository.getUserById(id));
        }
        public async Task<List<UserDTO>> getAllUsers()
        {
            return _mapper.Map<List<User>, List<UserDTO>>(await userRepository.getAllUsers());
        }
    }
}
