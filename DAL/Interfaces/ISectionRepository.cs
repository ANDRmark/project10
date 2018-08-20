using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISectionRepository: IRepository<Section>
    {
        Section GetByIdWithThemes(int id);
        IEnumerable<Section> SearchByNamePart(string sectionNamePart);
    }
}
