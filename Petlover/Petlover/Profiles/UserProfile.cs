using AutoMapper;
using Petlover.Models;
using Petlover.ViewModels.UserVMs;

namespace Petlover.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<RegisterVM, User>();
        CreateMap<LoginVM, User>();
    }
}
