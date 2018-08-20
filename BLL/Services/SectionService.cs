using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class SectionService : ISectionService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public SectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        void ISectionService.Delete(int id)
        {
            this.unitOfWork.Sections.Delete(id);
            this.unitOfWork.Save();
        }

        IEnumerable<SectionDTO> ISectionService.GetAll()
        {
            IEnumerable<DAL.Models.Section> sections = this.unitOfWork.Sections.GetAll();
            return this.mapper.Map<IEnumerable<SectionDTO>>(sections);
        }

        SectionDTO ISectionService.GetById(int id)
        {
            DAL.Models.Section section = this.unitOfWork.Sections.GetById(id);
            return this.mapper.Map<SectionDTO>(section);
        }

        SectionDTO ISectionService.GetByIdWithThemes(int id)
        {
            DAL.Models.Section section = this.unitOfWork.Sections.GetByIdWithThemes(id);
            return this.mapper.Map<SectionDTO>(section);
        }

        IEnumerable<SectionDTO> ISectionService.SearchByNamePart(string sectionNamePart)
        {
            IEnumerable<DAL.Models.Section> sections = this.unitOfWork.Sections.SearchByNamePart(sectionNamePart);
            return this.mapper.Map<IEnumerable<SectionDTO>>(sections);
        }

        void ISectionService.Insert(SectionDTO newSection)
        {
            DAL.Models.Section section = this.mapper.Map<DAL.Models.Section>(newSection);
            this.unitOfWork.Sections.Insert(section);
            this.unitOfWork.Save();
        }


        void ISectionService.Update(SectionDTO sectionToUpdate)
        {
            DAL.Models.Section section = this.mapper.Map<DAL.Models.Section>(sectionToUpdate);
            this.unitOfWork.Sections.Update(section);
            this.unitOfWork.Save();
        }
    }
}
