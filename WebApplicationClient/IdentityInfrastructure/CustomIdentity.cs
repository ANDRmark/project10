using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace WebApplicationClient.IdentityInfrastructure
{
    public class CustomUserManager: UserManager<User, int>
    {
        public CustomUserManager(IUserStore<User, int> store):base(store)
        {

        }
        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        {
            return new CustomUserManager(new CustomUserStore((IUserInfoService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserInfoService))));
        }

    }

    public class User : UserInfoDTO, IUser<int>
    {
        public static User FromDTO(UserInfoDTO userDTO)
        {
            if (userDTO == null) return null;
            return new User()
            {
                Id = userDTO.Id,
                UserName = userDTO.UserName,
                PasswordHash = userDTO.PasswordHash,
                Email = userDTO.Email
            };
        }

        internal async Task<ClaimsIdentity> GenerateUserIdentityAsync(CustomUserManager userManager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await userManager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class CustomUserStore : IUserStore<User, int>, IUserPasswordStore<User, int>
    {
        IUserInfoService userInfoService;

        public CustomUserStore(IUserInfoService userInfoService)
        {
            this.userInfoService = userInfoService;
        }

        Task IUserStore<User, int>.CreateAsync(User user)
        {
            this.userInfoService.Insert(user);
            return Task.FromResult(0);
        }

        Task IUserStore<User, int>.DeleteAsync(User user)
        {
            this.userInfoService.Delete(user.Id);
            return Task.FromResult(0);
        }

        Task<User> IUserStore<User, int>.FindByIdAsync(int userId)
        {
            return Task.FromResult(User.FromDTO(this.userInfoService.GetById(userId)));
        }

        Task<User> IUserStore<User, int>.FindByNameAsync(string userName)
        {
            return Task.FromResult(User.FromDTO(this.userInfoService.GetByUsername(userName)));
        }

        Task IUserStore<User, int>.UpdateAsync(User user)
        {
            this.userInfoService.Update(user);
            return Task.FromResult(0);
        }

        Task IUserPasswordStore<User, int>.SetPasswordHashAsync(User user, string passwordHash)
        {
            if(user == null)
            {
                throw new ArgumentException("user");
            }
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        Task<string> IUserPasswordStore<User, int>.GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        Task<bool> IUserPasswordStore<User, int>.HasPasswordAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var hasPassword = !string.IsNullOrEmpty(user.PasswordHash);

            return Task.FromResult(hasPassword);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CustomUserStore() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}