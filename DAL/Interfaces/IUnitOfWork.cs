﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IThemeRepository Themes { get; }
        IMessageRepository Messages { get; }
        IUserInfoRepository Users { get;  }
        void Save();
    }
}
