using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplicationClient.Controllers
{
    [RoutePrefix("api/Forum")]
    public class ForumController : ApiController
    {
        private readonly IMessageService messageService;

        public ForumController(IMessageService messageService)
        {
            this.messageService = messageService;
        }


        //GET api/Forum/GetMessages/50
        [Route("GetMessages/{themeId:int}")]
        public IHttpActionResult GetMessages(int themeId)
        {

            return Ok(new { theme = themeId,body = this.messageService.GetAll() });
        }
    }
}
