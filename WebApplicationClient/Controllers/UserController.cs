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
    //[Authorize(Roles = "Admin")]
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
        [Route("GetUsesByUserName")]
        public IHttpActionResult GetUsesByUserName(string username) 
        {
            if (ModelState.IsValid)
            {
                List<UserInfoDTO> users = this.userInfoService.SearchByUsername(username).ToList();
                return Ok(new { users = users });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //GetUsesById

        [HttpGet]
        [Route("GetUsesById")]
        public IHttpActionResult GetUsesById(int? userid)
        {
            if (ModelState.IsValid && userid != null)
            {
                List<UserInfoDTO> users = new UserInfoDTO[] { this.userInfoService.GetById(userid.Value) }.ToList();
                return Ok(new { users = users });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
