using AutoMapper;
using Wings.Shared.Dvo;
using System.Linq;
namespace Wings.Shared
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