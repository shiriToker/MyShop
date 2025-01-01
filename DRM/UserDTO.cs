using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserGetByIdDTO(string UserName, string FirstName, string LastName);

    public record UserCreateDTO(string UserName, string FirstName, string LastName,string Password);
    public record UserUpdateDTO(string UserName, string FirstName, string LastName, string Password);




}
