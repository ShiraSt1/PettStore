using DTOs;

namespace Services
{
    public interface IUserService
    {
        Task<UserDTO> addUser(UserRegisterDTO user);
        Task<List<UserDTO>> getAllUsers();
        Boolean GetPassStrength(string password);
        Task<UserDTO> getUserById(int id);
        Task<UserDTO> login(UserLoginDTO newUser);
        Task<UserDTO> updateUser(int id, UserRegisterDTO userUpdate);
    }
}