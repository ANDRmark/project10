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
        private readonly IRoleService roleService;

        private const string LocalLoginProvider = "Local";
        private CustomUserManager _userManager;

        public UserController(IUserInfoService userInfoService, IRoleService roleService)
        {
            this.userInfoService = userInfoService;
            this.roleService = roleService;
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

        // GET  /api/User/SearchUsersByUserNamePart&username=andr
        [HttpGet]
        [Route("SearchUsersByUserNamePart")]
        public IHttpActionResult SearchUsersByUserNamePart(string username) 
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

        // GET  /api/User/3
        [HttpGet]
        [Route("{userid:int}")]
        public IHttpActionResult GetUserById(int userid)
        {
            if (ModelState.IsValid)
            {
                UserInfoDTO user = this.userInfoService.GetById(userid);
                return Ok(new { user = user });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // GET  /api/User/GetAll
        [HttpGet]
        [Route("GetAll")]
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

        // POST  /api/User
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateUser([FromBody] UpdateUserBindingModel model)
        {
            if (ModelState.IsValid)
            {
                UserInfoDTO user = this.userInfoService.GetById(model.Id);
                if (user == null) return BadRequest("User not found");

                user.UserName = model.UserName;
                user.Email = model.Email;

                user.Roles = new List<RoleDTO>();
                foreach(var role in model.Roles)
                {
                    user.Roles.Add(new RoleDTO() { Name = role.Name });
                }
                this.userInfoService.Update(user);

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        // DELETE  /api/User/7
        [HttpDelete]
        [Route("{userId:int}")]
        public IHttpActionResult DeleteUser(int userId)
        {
            if (ModelState.IsValid)
            {
                this.userInfoService.Delete(userId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
