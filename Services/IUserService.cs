using Entities;

namespace Services
{
    public interface IUserService
    {
        User addUser(User user);
        int GetPassStrength(string password);
        User login(User newUser);
        User updateUser(int id, User userUpdate);
    }
}