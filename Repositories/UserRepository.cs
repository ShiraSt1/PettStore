
using Entities;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        //public User getUserById(int id){}

        public User addUser(User user)
        {
            int numberOfUsers = System.IO.File.ReadLines("C:\\Users\\shiri\\Desktop\\14th grade\\API\\pettsStore\\users.txt").Count();
            user.userId = numberOfUsers + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText("C:\\Users\\shiri\\Desktop\\14th grade\\API\\pettsStore\\users.txt", userJson + Environment.NewLine);
            return user;
        }

        public User updateUser(int id, User userUpdate)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText("C:\\Users\\shiri\\Desktop\\14th grade\\API\\pettsStore\\users.txt"))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {

                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.userId == id)
                        textToReplace = currentUserInFile;
                }
            }

            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText("C:\\Users\\shiri\\Desktop\\14th grade\\API\\pettsStore\\users.txt");
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userUpdate));
                System.IO.File.WriteAllText("C:\\Users\\shiri\\Desktop\\14th grade\\API\\pettsStore\\users.txt", text);
            }

            return userUpdate;
        }

        public User login(User newUser)
        {
            using (StreamReader reader = System.IO.File.OpenText("C:\\Users\\shiri\\Desktop\\14th grade\\API\\pettsStore\\users.txt"))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.username == newUser.username && user.password == newUser.password)
                        return user;
                }
            }
            return null;
        }

    }
}
