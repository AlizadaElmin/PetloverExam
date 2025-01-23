using System.ComponentModel.DataAnnotations;

namespace Petlover.ViewModels.UserVMs;

public class LoginVM
{
    [Required,MaxLength(96)]
    public string UsernameOrEmail { get; set; }
    [Required,DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }

}
