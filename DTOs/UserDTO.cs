using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record UserDTO(int Id, string FirstName, string LastName, string UserName);
    public record UserRegisterDTO(string FirstName, string LastName, string Password, string UserName);
    public record UserLoginDTO(string Password, string UserName);
}
