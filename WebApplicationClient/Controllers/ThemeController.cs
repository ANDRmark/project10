using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplicationClient.Controllers
{
    [RoutePrefix("api/Theme")]
    public class ThemeController : ApiController
    {
        private readonly IMessageService messageService;
        private readonly IThemeService themeService;

        public ThemeController(IMessageService messageService, IThemeService themeService)
        {
            this.messageService = messageService;
            this.themeService = themeService;
        }


        //GET api/Theme/GetTheme/3
        [Route("GetTheme/{themeId:int}")]
        public IHttpActionResult GetTheme(int themeId)
        {
            return Ok(new { theme = this.themeService.GetById(themeId) });
        }


        //GET api/Theme/GetThemes
        [Route("GetThemes")]
        public IHttpActionResult GetThemes()
        {
            return Ok(new { themes = this.themeService.GetAll() });
        }
    }
}
