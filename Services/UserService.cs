using Entities;
using Repositories;
using System.Text.Json;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User addUser(User user)
        {
            return userRepository.addUser(user);
        }

        public User updateUser(int id, User userUpdate)
        {
            return userRepository.updateUser(id, userUpdate);
        }

        public User login(User newUser)
        {
            return userRepository.login(newUser);
        }

        public int GetPassStrength(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }
    }
}
