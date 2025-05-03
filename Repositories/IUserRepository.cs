using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        User addUser(User user);
        User login(User newUser);
        User updateUser(int id, User userUpdate);
    }
}