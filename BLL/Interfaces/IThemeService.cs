using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IThemeService
    {
        IEnumerable<ThemeDTO> GetAll();
        ThemeDTO GetById(int id);
        void Insert(ThemeDTO newTheme);
        void Update(ThemeDTO theme);
        void Delete(int id);
    }
}
