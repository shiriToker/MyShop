using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class User
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
     
        [StringLength(20, ErrorMessage = "FirstName must be between 2 to 20 letters")]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "LastName must be between 2 to 20 letters")]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public int UserId { get; set; }



    }
}
