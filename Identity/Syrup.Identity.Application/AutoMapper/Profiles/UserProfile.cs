using AutoMapper;
using Syrup.Identity.Application.Dtos.Responses;
using Syrup.Identity.Core.Db.Entities;

namespace Syrup.Identity.Application.AutoMapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>();
    }
}
