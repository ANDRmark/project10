using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTO;
using BLL.Interfaces;
using WebApplicationClient.Models;

namespace WebApplicationClient.Controllers
{
    [RoutePrefix("api/Section")]
    public class SectionController : ApiController
    {
        private readonly ISectionService sectionService;
        public SectionController(ISectionService sectionService)
        {
            this.sectionService = sectionService;
        }
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAllSections()
        {
            if (ModelState.IsValid)
            {
                return Ok(new { sections = this.sectionService.GetAll() });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //GET api/Section/3
        [HttpGet]
        [Route("{sectionId:int}")]
        public IHttpActionResult GetSection(int sectionId)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { section = this.sectionService.GetById(sectionId) });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //GET api/Section/GetSectionsByNamePart&sectionName=autom
        [HttpGet]
        [Route("GetSectionsByNamePart")]
        public IHttpActionResult GetSectionsByNamePart(string sectionName)
        {
            if (ModelState.IsValid)
            {
                List<SectionDTO> sections = this.sectionService.SearchByNamePart(sectionName).ToList();
                return Ok(new { sections = sections });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //POST api/Section
        [Authorize(Roles = "User")]
        [Route("")]
        [HttpPost]
        public IHttpActionResult InsertNewSection([FromBody] NewSectionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                SectionDTO section = new SectionDTO();
                section.Title = model.SectionName;
                section.CreateDate = DateTime.Now;
                this.sectionService.Insert(section);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT api/Section
        [Authorize(Roles = "Moderator")]
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateSection([FromBody] UpdateSectionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                SectionDTO section = new SectionDTO();
                section.Id = model.Id;
                section.Title = model.Title;
                section.CreateDate = model.CreateDate;
                this.sectionService.Update(section);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //DELETE api/Section/4
        [Authorize(Roles = "Moderator")]
        [HttpDelete]
        [Route("{sectionId:int}")]
        public IHttpActionResult DeleteSection(int sectionId)
        {
            if (ModelState.IsValid)
            {
                this.sectionService.Delete(sectionId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
