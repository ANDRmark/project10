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

        //GetMessage
        //GET api/Message/50
        [HttpGet]
        [Route("{messageId:int}")]
        public IHttpActionResult GetMessage(int messageId)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { message = this.messageService.GetById(messageId) });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //GET api/Message/GetMessagesByThemeId/50
        [HttpGet]
        [Route("GetMessagesByThemeId/{themeId:int}")]
        public IHttpActionResult GetMessages(int themeId)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { theme = themeId, messages = this.messageService.GetByThemeId(themeId) });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //POST api/Message
        [Authorize(Roles = "User")]
        [Route("")]
        [HttpPost]
        public IHttpActionResult InsertNewMessage([FromBody] NewMessageBindingModel newMessage)
        {
            if (ModelState.IsValid)
            {
                MessageDTO m = new MessageDTO();
                m.MessageBody = newMessage.MessageBody;
                m.ThemeId = newMessage.ThemeId;
                m.CreateDate = DateTime.Now;
                m.UserId = int.Parse(User.Identity.GetUserId());
                this.messageService.Insert(m);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //GET api/Message/SearchMessages&themeId=2
        [HttpGet]
        [Route("SearchMessages")]
        public IHttpActionResult SearchMessages(int? themeId, string userName = null, string messageBody = null)
        {
            if (ModelState.IsValid)
            {
                var messages = this.messageService.Search(themeId.Value, userName: userName, messageBody:messageBody);
                return Ok(new { theme = themeId, messages = messages });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/Message
        [Authorize(Roles = "Moderator")]
        [Route("")]
        [HttpPut]
        public IHttpActionResult UpdateMessage([FromBody] MessageToUpdateBindingModel messagetoUpdate)
        {
            if (ModelState.IsValid)
            {
                MessageDTO m = new MessageDTO();
                m.Id = messagetoUpdate.Id;
                m.MessageBody = messagetoUpdate.MessageBody;
                m.ThemeId = messagetoUpdate.ThemeId;
                m.CreateDate = messagetoUpdate.CreateDate;
                m.UserId = int.Parse(User.Identity.GetUserId());
                this.messageService.Update(m);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //DELETE api/Message/2
        [HttpDelete]
        [Route("{MessageId:int}")]
        [Authorize(Roles = "Moderator")]
        public IHttpActionResult DeleteMessage(int MessageId)
        {
            if (ModelState.IsValid)
            {
                this.messageService.Delete(MessageId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
