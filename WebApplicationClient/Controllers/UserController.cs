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
using WebApplicationClient.Models;

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

        // GET  /api/User/GetUsesByUserName&username=andr
        [HttpGet]
        [Route("GetUsersByUserNamePart")]
        public IHttpActionResult GetUsersByUserNamePart(string username) 
        {
            if (ModelState.IsValid)
            {
                List<UserInfoDTO> users = this.userInfoService.SearchByUsernamePart(username).ToList();
                return Ok(new { users = users });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET  /api/User/GetUsesById&userid=3
        [HttpGet]
        [Route("GetUserById")]
        public IHttpActionResult GetUserById(int? userid)
        {
            if (ModelState.IsValid)
            {
                UserInfoDTO user = this.userInfoService.GetById(userid.Value);
                return Ok(new { user = user });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET  /api/User/GetAllUsers
        [HttpGet]
        [Route("GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            if (ModelState.IsValid)
            {
                List<UserInfoDTO> users = this.userInfoService.GetAll().ToList();
                return Ok(new { users = users });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // POST  /api/User/SetRoles
        [HttpPost]
        [Route("SetRoles")]
        public IHttpActionResult SetRoles([FromBody] SetRolesBindingModel model)
        {
            if (ModelState.IsValid)
            {
                UserInfoDTO user =  this.userInfoService.GetById(model.UserId);
                if (user == null) return BadRequest("User not found");
                if (model.Roles.IsUser)
                {
                    UserManager.AddToRole(user.Id, "User");
                }
                else
                {
                    UserManager.RemoveFromRole(user.Id, "User");
                }
                if (model.Roles.IsModerator)
                {
                    UserManager.AddToRole(user.Id, "Moderator");
                }
                else
                {
                    UserManager.RemoveFromRole(user.Id, "Moderator");
                }
                if (model.Roles.IsAdmin)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
                else
                {
                    UserManager.RemoveFromRole(user.Id, "Admin");
                }
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DeleteUser
        // POST  /api/User/DeleteUser
        [HttpPost]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser([FromBody] DeleteUserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                this.userInfoService.Delete(model.UserId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
