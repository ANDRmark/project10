﻿using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISectionService
    {
        IEnumerable<SectionDTO> GetAll();
        SectionDTO GetById(int id);
        SectionDTO GetByIdWithThemes(int id);
        void Insert(SectionDTO newSection);
        void Update(SectionDTO section);
        void Delete(int id);
    }
}
