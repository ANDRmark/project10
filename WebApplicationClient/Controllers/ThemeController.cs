using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationClient.Models;

namespace WebApplicationClient.Controllers
{
    [RoutePrefix("api/Theme")]
    public class ThemeController : ApiController
    {
        private readonly IMessageService messageService;
        private readonly IThemeService themeService;
        private readonly IUserInfoService userInfoService;

        public ThemeController(IMessageService messageService, IThemeService themeService, IUserInfoService userInfoService)
        {
            this.messageService = messageService;
            this.themeService = themeService;
            this.userInfoService = userInfoService;
        }


        //GET api/Theme/GetTheme/3
        [Route("GetTheme/{themeId:int}")]
        public IHttpActionResult GetTheme(int themeId)
        {
            return Ok(new { theme = this.themeService.GetById(themeId) });
        }


        //GET api/Theme/GetThemesBySection/1
        [Route("GetThemesBySection/{sectionId:int}")]
        public IHttpActionResult GetThemes(int sectionId)
        {
            return Ok(new { themes = this.themeService.GetBySection(sectionId) });
        }


        //Post api/Theme/InsertNewTheme/1
        [Authorize]
        [Route("InsertNewTheme")]
        [HttpPost]
        public IHttpActionResult InsertNewTheme([FromBody] NewThemeBindingModel newTheme)
        {
            if (ModelState.IsValid)
            {
                ThemeDTO t = new ThemeDTO();
                t.CreateDate = DateTime.Now;
                t.Title = newTheme.ThemeName;
                t.SectionId = newTheme.SectionId;
                this.themeService.Insert(t);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
