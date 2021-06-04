using AutoMapper;
using Wings.Api.Models;
using Wings.Shared.Dvo;
using System.Linq;
using Wings.Shared.Dto;
using Wings.Framework.Shared.Dtos;

namespace Wings.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Menu, MenuListDvo>()

            .ForMember((dvo) => dvo.Title, opt => opt.MapFrom(menu => menu.Name))
            .ForMember((dvo) => dvo.Children, opt => opt.MapFrom(menu => menu.Children))
            ;
            CreateMap<Role, RoleListDvo>()
            .ForMember((roleListDvo) => roleListDvo.MenuList, opt => opt.MapFrom(role => role.Menus))
            ;
            CreateMap<Menu, MenuData>()
            .ForMember((dvo) => dvo.Label, opt => opt.MapFrom(m => m.Name))
            .ForMember((dvo) => dvo.Link, opt => opt.MapFrom(m => m.Path))
            ;
        }
    }
}