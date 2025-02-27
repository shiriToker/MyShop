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
        [StringLength(100, ErrorMessage = "FirstName must be till 100 letters")]
        string FirstName,
        [StringLength(100, ErrorMessage = "LastName must be till 100 letters")]
        string LastName,
        [Required]
        string Password);
}
