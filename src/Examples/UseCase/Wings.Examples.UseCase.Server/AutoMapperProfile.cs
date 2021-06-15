using AutoMapper;
using System.Linq;
using Wings.Examples.UseCase.Shared.Dto;
using Wings.Framework.Shared.Dtos;
using Wings.Examples.UseCase.Shared.Dvo;
using Wings.Examples.UseCase.Server.Models;
using Wings.Examples.UseCase.Server.Controllers.Admin;

namespace Wings.Examples.UseCase.Server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Menu, MenuListDvo>()

            .ForMember((dvo) => dvo.Title, opt => opt.MapFrom(menu => menu.Name))
              .ForMember((dvo) => dvo.Path, opt => opt.MapFrom(m => m.Url))
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
            CreateMap<Menu, MenuCreateDvo>()
           .ForMember((dvo) => dvo.Path, opt => opt.MapFrom(m => m.Url))
           .ForMember((dvo) => dvo.Title, opt => opt.MapFrom(m => m.Name))
           .ReverseMap()
           ;
            CreateMap<Permission, PermissionListDvo>()
                .ForMember(dvo=>dvo.Title,opt=>opt.MapFrom(m=>m.Label))
                .ReverseMap();

            CreateMap<Category, CategoryListDvo>()
                .ForMember(dvo => dvo.Title, entity => entity.MapFrom(m => m.Name))
                      .ForMember(dvo => dvo.Attrs, entity => entity.MapFrom(m => m.Attrs))
                .ReverseMap();

            CreateMap<Attr, AttrListDvo>()
               .ForMember(dvo=>dvo.AttrCategoryId,opt=>opt.MapFrom(entity=>entity.AttrCategoryId))
               .ForMember(dvo=>dvo.AttrCategoryOption,opt=>opt.MapFrom(entity=>entity.AttrCategory))
               .ReverseMap();
            CreateMap<AttrCategory, AttrCategoryOption>()
           .ForMember(dvo => dvo.Label, entity => entity.MapFrom(m => m.Name))
           .ReverseMap();
            CreateMap<AttrCategory, AttrCategoryListDvo>().ReverseMap();
            //CreateMap<AttrWithAttrCatetory, AttrListDvo>()
            //    .ForMember(attr => attr.AttrCategoryOption, withCateory => withCateory.MapFrom(opt => opt.AttrCategoryOption))
            //    .ForMember(attr => attr, withCateory => withCateory.MapFrom(opt => opt.AttrListDvo)).ReverseMap();
        }
    }
}