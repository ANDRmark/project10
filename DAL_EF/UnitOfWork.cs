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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Lazy<IMessageRepository> lazyMessages;
        private readonly Lazy<IThemeRepository> lazyThemes;
        private readonly Lazy<IUserInfoRepository> lazyUsers;
        private readonly Lazy<IRoleRepository> lazyRoles;
        private readonly Lazy<ISectionRepository> lazySections;

        private readonly Lazy<ForumDbContext> lazydbContext;

        public UnitOfWork(string connectionString)
        {
            this.lazydbContext = new Lazy<ForumDbContext>(() => new ForumDbContext(connectionString),true);
            this.lazyMessages = new Lazy<IMessageRepository>(() => new MessageRepository(this.dbContext), true);
            this.lazyThemes = new Lazy<IThemeRepository>(() => new ThemeRepository(this.dbContext), true);
            this.lazyUsers = new Lazy<IUserInfoRepository>(() => new UserInfoRepository(this.dbContext), true);
            this.lazyRoles = new Lazy<IRoleRepository>(() => new RoleRepository(this.dbContext), true);
            this.lazySections = new Lazy<ISectionRepository>(() => new SectionRepository(this.dbContext), true);
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

        IRoleRepository IUnitOfWork.Roles
        {
            get
            {
                return this.lazyRoles.Value;
            }
        }

        ISectionRepository IUnitOfWork.Sections
        {
            get
            {
                return this.lazySections.Value;
            }
        }

        void IUnitOfWork.Save()
        {
            this.dbContext.SaveChanges();
        }


        private bool disposed = false; 
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.dbContext.Dispose();
                }

                disposed = true;
            }
        }
        void IDisposable.Dispose()
        {
            Dispose(true);

        }
    }
}
