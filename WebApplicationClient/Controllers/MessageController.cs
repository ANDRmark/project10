using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplicationClient.Controllers
{
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;
        private readonly IThemeService themeService;

        public MessageController(IMessageService messageService, IThemeService themeService)
        {
            this.messageService = messageService;
            this.themeService = themeService;
        }


        //GET api/Message/GetMessagesByThemeId/50
        [Route("GetMessagesByThemeId/{themeId:int}")]
        public IHttpActionResult GetMessages(int themeId)
        {
            return Ok(new { theme = themeId, messages = this.messageService.GetByThemeId(themeId) });
        }


    }
}
