using AutoMapper;
using Syrup.Application.Dtos.Responses;
using Syrup.Core.Db.Entities;

namespace Syrup.Application.AutoMapper.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<Company, CompanyResponse>();
    }
}
