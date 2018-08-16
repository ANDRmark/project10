using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationClient.IdentityInfrastructure;

namespace WebApplicationClient.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserInfoService userInfoService;

        private const string LocalLoginProvider = "Local";
        private CustomUserManager _userManager;

        public UserController(IUserInfoService userInfoService)
        {
            this.userInfoService = userInfoService;
        }

        public UserController(CustomUserManager userManager,
            IUserInfoService userInfoService)
        {
            UserManager = userManager;
            this.userInfoService = userInfoService;
        }


        public CustomUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<CustomUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Route("GetUsesByUsername")]
        public IHttpActionResult GetUsesByUsername(string username) 
        {
            if (String.IsNullOrEmpty(username))
            {
                return BadRequest();
            }
            List<UserInfoDTO> users =  this.userInfoService.SearchByUsername(username).ToList();
            return Ok(new { users=users});
        }
    }
}
