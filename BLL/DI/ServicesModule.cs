﻿using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DI
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<BLL.Interfaces.IMessageService>().To<BLL.Services.MessageService>();
            this.Bind<BLL.Interfaces.IThemeService>().To<BLL.Services.ThemeService>();
        }
    }
}
