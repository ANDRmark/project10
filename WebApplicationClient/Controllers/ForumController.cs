using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WebApplicationClient.Controllers
{
    [RoutePrefix("api/Forum")]
    public class ForumController : ApiController
    {
        private readonly IMessageService messageService;
        private readonly IThemeService themeService;

        public ForumController(IMessageService messageService, IThemeService themeService)
        {
            this.messageService = messageService;
            this.themeService = themeService;
        }

        [Authorize]
        //GET api/Forum/GetMessages/50
        [Route("GetMessages/{themeId:int}")]
        public IHttpActionResult GetMessages(int themeId)
        {
            return Ok(new { theme = themeId,body = this.messageService.GetByThemeId(themeId) });
        }


        //GET api/Forum/GetThemes
        [Route("GetThemes")]
        public IHttpActionResult GetThemes()
        {
            return Ok(new { themes=this.themeService.GetAll()});
        }
    }
}
