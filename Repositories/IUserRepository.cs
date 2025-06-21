using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> addUser(User user);
        Task<User> login(User newUser);
        Task<User> updateUser(int id, User userUpdate);
        Task<User> getUserById(int id);
        Task<List<User>> getAllUsers();
    }
}