﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using BLL.DTO;

namespace BLL.DI
{
    public class MapperModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            IConfigurationProvider mapperconfig = new MapperConfiguration(
                cfg => cfg.AddProfile<AutoMapperProfileForDTOs>()
                );

            this.Bind<IMapper>().ToMethod(ctx => new Mapper(mapperconfig));
        }
    }

    class AutoMapperProfileForDTOs : Profile
    {
        public AutoMapperProfileForDTOs()
        {
            this.CreateMap<Message, MessageDTO>().ForMember(m => m.UserName, opt => opt.MapFrom(m => m.User != null? m.User.UserName:null)).ReverseMap().ForMember(m => m.User, opt => opt.Ignore()).ForMember(m => m.Theme, opt => opt.Ignore());
            this.CreateMap<Theme, ThemeDTO>().ReverseMap();
            this.CreateMap<Role, RoleDTO>().ReverseMap().ForMember(r => r.Users, opt => opt.Ignore());
            this.CreateMap<UserInfo, UserInfoDTO>().ReverseMap();
            this.CreateMap<Section, SectionDTO>();
        }
    }
}
