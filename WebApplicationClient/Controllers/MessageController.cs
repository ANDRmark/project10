using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationClient.Models;

namespace WebApplicationClient.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;
        private readonly IThemeService themeService;
        private readonly IUserInfoService userInfoService;

        public MessageController(IMessageService messageService, IThemeService themeService, IUserInfoService userInfoService)
        {
            this.messageService = messageService;
            this.themeService = themeService;
            this.userInfoService = userInfoService;
        }


        //GET api/Message/GetMessagesByThemeId/50
        [Route("GetMessagesByThemeId/{themeId:int}")]
        public IHttpActionResult GetMessages(int themeId)
        {
            return Ok(new { theme = themeId, messages = this.messageService.GetByThemeId(themeId) });
        }

        //Post api/Message/InsertNewMessage
        [Authorize]
        [Route("InsertNewMessage")]
        [HttpPost]
        public IHttpActionResult InsertNewMessage([FromBody] NewMessageBindingModel newMessage)
        {
            if (ModelState.IsValid)
            {
                UserInfoDTO userinfo = this.userInfoService.GetByExternalId(User.Identity.GetUserId());
                MessageDTO m = new MessageDTO();
                m.MessageBody = newMessage.MessageBody;
                m.ThemeId = newMessage.ThemeId;
                m.CreateDate = DateTime.Now;
                m.UserId = userinfo.Id;
                this.messageService.Insert(m);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}
