using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IThemeRepository :IRepository<Theme>
    {
        IEnumerable<Theme> GetBySection(int sectionId);
        IEnumerable<Theme>  SearchThemesByNamePart(string namePart);
    }
}
