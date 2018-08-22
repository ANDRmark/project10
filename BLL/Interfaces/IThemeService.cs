using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
     public interface IThemeService
    {
        IEnumerable<ThemeDTO> GetAll();
        IEnumerable<ThemeDTO> GetBySection(int sectionId);
        ThemeDTO GetById(int id);
        IEnumerable<ThemeDTO> SearchThemesByNamePart(string namePart);
        void Insert(ThemeDTO newTheme);
        void Update(ThemeDTO theme);
        void Delete(int id);
    }
}
