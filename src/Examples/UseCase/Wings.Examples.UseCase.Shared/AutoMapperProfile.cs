using AutoMapper;
using System.Linq;
using Wings.Examples.UseCase.Shared.Dvo;

namespace Wings.Examples.UseCase.Shared
{
    public class SharedAutoMapperProfile : Profile
    {
        public SharedAutoMapperProfile()
        {
            CreateMap<MenuListDvo, MenuCreateDvo>()
            .ReverseMap();
        }
    }
}