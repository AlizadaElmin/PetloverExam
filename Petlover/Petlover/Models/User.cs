using Microsoft.AspNetCore.Identity;

namespace Petlover.Models;

public class User:IdentityUser
{
    public string FullName { get; set; }
}
