using System;
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
        private readonly Lazy<IMessageRepository> lazyMessages;
        private readonly Lazy<IThemeRepository> lazyThemes;
        private readonly Lazy<IUserInfoRepository> lazyUsers;

        private readonly Lazy<ForumDbContext> lazydbContext;

        public UnitOfWork(string connectionString)
        {
            this.lazydbContext = new Lazy<ForumDbContext>(() => new ForumDbContext(connectionString),true);
            this.lazyMessages = new Lazy<IMessageRepository>(() => new MessageRepository(this.dbContext), true);
            this.lazyThemes = new Lazy<IThemeRepository>(() => new ThemeRepository(this.dbContext), true);
            this.lazyUsers = new Lazy<IUserInfoRepository>(() => new UserInfoRepository(this.dbContext), true);
        }

        private ForumDbContext dbContext
        {
            get
            {
                return this.lazydbContext.Value;
            }
        }


        IMessageRepository IUnitOfWork.Messages
        {
            get
            {
                return this.lazyMessages.Value;
            }
        }

        IThemeRepository IUnitOfWork.Themes
        {
            get
            {
                return this.lazyThemes.Value;
            }
        }
        IUserInfoRepository IUnitOfWork.Users
        {
            get
            {
                return this.lazyUsers.Value;
            }
        }

        void IUnitOfWork.Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
