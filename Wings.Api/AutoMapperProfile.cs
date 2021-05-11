using AutoMapper;
using Wings.Api.Models;
using Wings.Shared.Dvo;
using System.Linq;
namespace Wings.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Menu, MenuListDvo>();
            CreateMap<Menu, MenuDto>();
            CreateMap<Menu, MenuListDvo>()
            .ForMember((dvo) => dvo.Title, opt => opt.MapFrom(menu => menu.Name))
            .ForMember((dvo) => dvo.Children, opt => opt.MapFrom(menu => menu.Children))

            ;
        }
    }
}