using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        PettsStore_DataBaseContext _pettsStore_DataBaseContext;
        public UserRepository(PettsStore_DataBaseContext pettsStore_DataBaseContext)
        {
            _pettsStore_DataBaseContext = pettsStore_DataBaseContext;
        }
        public async Task<User> GetUserById(int id) // Changed to PascalCase
        {
            User user= await _pettsStore_DataBaseContext.Users.FirstOrDefaultAsync(user=>user.Id==id);
            return user;
        }
        public async Task<List<User>> GetAllUsers() // Changed to PascalCase
        {
            return await _pettsStore_DataBaseContext.Users.ToListAsync();
        }
        public async Task<User> AddUser(User user) // Changed to PascalCase 
        {
            await _pettsStore_DataBaseContext.Users.AddAsync(user);
            await _pettsStore_DataBaseContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUser(int id, User userUpdate) // Changed to PascalCase
        {
            _pettsStore_DataBaseContext.Users.Update(userUpdate);
             await _pettsStore_DataBaseContext.SaveChangesAsync();
            return userUpdate;
         
        }
        public async Task<User> Login(User newUser)
        {
            User user=await _pettsStore_DataBaseContext.Users.FirstOrDefaultAsync(user => user.Username == newUser.Username && user.Password == newUser.Password);
            return user;
        }
    }
}