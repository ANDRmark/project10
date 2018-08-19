using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using WebApplicationClient.Models;
using WebApplicationClient.IdentityInfrastructure;
using DAL.Interfaces;
using System.Security.Cryptography;
using Microsoft.Owin.Security.Infrastructure;

namespace WebApplicationClient.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        private IUnitOfWork GetUnitOfWork()
        {
            return (IUnitOfWork)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUnitOfWork));
        }

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });




            //var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var userManager = context.OwinContext.GetUserManager<CustomUserManager>();

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);
            User user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "userName", user.UserName },
                { "as:client_id", context.ClientId ??string.Empty }
            });
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //// Resource owner password credentials does not provide a client ID.
            //if (context.ClientId == null)
            //{
            //    context.Validated();
            //}

            //return Task.FromResult<object>(null);



            string clientId = string.Empty;
            string clientSecret = string.Empty;
            DAL.Models.Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                //context.Validated();
                context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            IUnitOfWork unitOfWork = this.GetUnitOfWork();
            client = unitOfWork.Clients.GetById(int.Parse(context.ClientId));

            //using (IUnitOfWork unitOfWork = this.GetUnitOfWork())
            //{
            //    client = _repo.FindClient(context.ClientId);
            //}

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return Task.FromResult<object>(null);
            }

            if (client.ApplicationType == DAL.Models.ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return Task.FromResult<object>(null);
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return ;
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            //return Task.FromResult<object>(null);



            var userManager = context.OwinContext.GetUserManager<CustomUserManager>();
            User user = userManager.FindById(context.Ticket.Identity.GetUserId<int>());

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            //AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "userName", user.UserName },
                { "as:client_id", context.ClientId ??string.Empty }
            });
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }
    }

    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private IUnitOfWork GetUnitOfWork()
        {
            return (IUnitOfWork)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUnitOfWork));
        }
        public void Create(AuthenticationTokenCreateContext context)
        {
            var res = this.CreateAsync(context);
            res.Wait();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            IUnitOfWork unitOfWork =  GetUnitOfWork();
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");
            var token = new DAL.Models.RefreshToken()
            {
                //Id = Helper.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };
            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            unitOfWork.RefreshTokens.Insert(token);
            unitOfWork.Save();
            context.SetToken(token.Id.ToString());

        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            var res = this.ReceiveAsync(context);
            res.Wait();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = context.Token;

            IUnitOfWork unitOfWork = GetUnitOfWork();
            DAL.Models.RefreshToken refreshToken = unitOfWork.RefreshTokens.GetById(int.Parse(hashedTokenId));
            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                unitOfWork.RefreshTokens.Delete(int.Parse(hashedTokenId));
                unitOfWork.Save();
            }
        }
    }
    class Helper
    {
        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}