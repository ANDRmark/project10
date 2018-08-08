﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;
using DAL_EF.Repositories;

namespace DAL_EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IRepository<Message>> lazyMessages;
        private readonly Lazy<IRepository<Theme>> lazyThemes;

        private readonly Lazy<ForumDbContext> lazydbContext;

        public UnitOfWork(string connectionString)
        {
            this.lazydbContext = new Lazy<ForumDbContext>(() => new ForumDbContext(connectionString));
            this.lazyMessages = new Lazy<IRepository<Message>>(() => new MessageRepository(this.dbContext), true);
            this.lazyThemes = new Lazy<IRepository<Theme>>(() => new ThemeRepository(this.dbContext), true);
        }

        private ForumDbContext dbContext
        {
            get
            {
                return this.lazydbContext.Value;
            }
        }


        IRepository<Message> IUnitOfWork.Messages
        {
            get
            {
                return this.lazyMessages.Value;
            }
        }

        IRepository<Theme> IUnitOfWork.Themes
        {
            get
            {
                return this.lazyThemes.Value;
            }
        }

        void IUnitOfWork.Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
