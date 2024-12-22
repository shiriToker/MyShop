using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity;

public partial class User
{
    public int UserId { get; set; }

    [Required]
    [EmailAddress]
    public string UserName { get; set; } = null!;
    [StringLength(20, ErrorMessage = "FirstName must be between 2 to 20 letters")]
    public string? FirstName { get; set; }
    [StringLength(20, ErrorMessage = "LastName must be between 2 to 20 letters")]

    public string? LastName { get; set; }
    [Required]
    public string Password { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
