using AutoMapper;
using Wings.Api.Models;
using System.Linq;
using Wings.Shared.Dto;
using Wings.Framework.Shared.Dtos;
using Wings.Examples.UseCase.Shared.Dvo;

namespace Wings.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Menu, MenuListDvo>()

            .ForMember((dvo) => dvo.Title, opt => opt.MapFrom(menu => menu.Name))
            .ReverseMap()
            ;
            CreateMap<Role, RoleListDvo>()
            .ForMember((roleListDvo) => roleListDvo.MenuList, opt => opt.MapFrom(role => role.Menus))
            .ReverseMap()
            ;
            CreateMap<Menu, MenuData>()
            .ForMember((dvo) => dvo.Label, opt => opt.MapFrom(m => m.Name))
            .ForMember((dvo) => dvo.Link, opt => opt.MapFrom(m => m.Url))
            .ReverseMap()
            ;
        }
    }
}