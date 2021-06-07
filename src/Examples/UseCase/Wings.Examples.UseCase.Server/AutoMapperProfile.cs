using AutoMapper;
using System.Linq;
using Wings.Examples.UseCase.Shared.Dto;
using Wings.Framework.Shared.Dtos;
using Wings.Examples.UseCase.Shared.Dvo;
using Wings.Examples.UseCase.Server.Models;

namespace Wings.Examples.UseCase.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Menu, MenuListDvo>()

            .ForMember((dvo) => dvo.Title, opt => opt.MapFrom(menu => menu.Name))
            .ReverseMap()
            ;
            CreateMap<RbacRole, RoleListDvo>()
            //.ForMember((roleListDvo) => roleListDvo.MenuList, opt => opt.MapFrom(role => role.Menus))
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