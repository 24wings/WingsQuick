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
            .ForMember((menuCreateDvo) => menuCreateDvo.ParentId, opt => opt.MapFrom(m => m.Id)).ReverseMap();

        }
    }
}