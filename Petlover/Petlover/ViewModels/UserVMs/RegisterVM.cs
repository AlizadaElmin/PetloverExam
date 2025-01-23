using System.ComponentModel.DataAnnotations;

namespace Petlover.ViewModels.UserVMs;

public class RegisterVM
{
    [Required, MaxLength(64)]
    public string FullName { get; set; }
    [Required, MaxLength(64)]
    public string Username { get; set; }
    [Required, MaxLength(96),DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Password { get; set; }
    [Required, DataType(DataType.Password), Compare("Password")]
    public string RePassword { get; set; }
}
