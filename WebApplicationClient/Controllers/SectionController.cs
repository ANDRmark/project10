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
        [Route("GetSections")]
        public IHttpActionResult GetSections()
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


        //GET api/Section/GetSection&sectionId=3
        [HttpGet]
        [Route("GetSection")]
        public IHttpActionResult GetSection(int? sectionId)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { section = this.sectionService.GetById(sectionId.Value) });
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

        //POST api/Section/InsertNewSection
        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("InsertNewSection")]
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


        //POST api/Section/RenameSection
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [Route("RenameSection")]
        public IHttpActionResult RenameSection([FromBody] RenameSectionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                SectionDTO section = this.sectionService.GetById(model.SectionId);
                section.Title = model.NewSectionName;
                this.sectionService.Update(section);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //POST api/Section/DeleteSection
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [Route("DeleteSection")]
        public IHttpActionResult DeleteSection([FromBody] DeleteSectionBindingModel model)
        {
            if (ModelState.IsValid)
            {
                this.sectionService.Delete(model.SectionId);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
