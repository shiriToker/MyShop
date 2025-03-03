using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public record UserGetByIdDTO(string UserName, string FirstName, string LastName);
    public record UserCreateDTO(
        [Required]
        [EmailAddress] string UserName,
        [StringLength(20, ErrorMessage = "FirstName must be till 20 letters")]
        string FirstName,
        [StringLength(20, ErrorMessage = "LastName must be till 20 letters")]
        string LastName,
        [Required]
        string Password);
}
