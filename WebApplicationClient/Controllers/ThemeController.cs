﻿using BLL.DTO;
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
        [HttpGet]
        public IHttpActionResult GetTheme(int themeId)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { theme = this.themeService.GetById(themeId) });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //GET api/Theme/GetThemesBySection/1
        [Route("GetThemesBySection/{sectionId:int}")]
        [HttpGet]
        public IHttpActionResult GetThemesBySection(int sectionId)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { themes = this.themeService.GetBySection(sectionId) });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        //GET api/Theme/GetAllThemes
        [Route("GetAllThemes")]
        [HttpGet]
        public IHttpActionResult GetAllThemes()
        {
            if (ModelState.IsValid)
            {
                return Ok(new { themes = this.themeService.GetAll() });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //GET api/Theme/SearchThemesByNamePart&namePart
        [Route("SearchThemesByNamePart")]
        [HttpGet]
        public IHttpActionResult SearchThemesByNamePart(string namePart)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { themes = this.themeService.SearchThemesByNamePart(namePart) });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //POST api/Theme/InsertNewTheme/1
        [Authorize(Roles = "User")]
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


        //POST api/Theme/UpdateTheme
        [Route("UpdateTheme")]
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IHttpActionResult UpdateTheme([FromBody] ThemeToUpdateBindingModel themetoUpdate)
        {
            if (ModelState.IsValid)
            {
                ThemeDTO t = new ThemeDTO();
                t.Id = themetoUpdate.Id;
                t.CreateDate = themetoUpdate.CreateDate;
                t.Title = themetoUpdate.Title;
                t.SectionId = themetoUpdate.SectionId;
                this.themeService.Update(t);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //ThemeId
        //POST api/Theme/DeleteTheme
        [Route("DeleteTheme")]
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IHttpActionResult DeleteTheme([FromBody] ThemeDeleteBindingModel themetoDelete)
        {
            if (ModelState.IsValid)
            {
                this.themeService.Delete(themetoDelete.ThemeId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
