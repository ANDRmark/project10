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
            this.CreateMap<Message, MessageDTO>().ReverseMap();
            this.CreateMap<Theme, ThemeDTO>().ReverseMap();
        }
    }
}
