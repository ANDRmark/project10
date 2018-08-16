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
            return Ok(new { sections = this.sectionService.GetAll() });
        }


        //GET api/Section/GetSection/3
        [HttpGet]
        [Route("GetSection/{sectionId:int}")]
        public IHttpActionResult GetSection(int sectionId)
        {
            return Ok(new { section = this.sectionService.GetById(sectionId) });
        }

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
    }
}
