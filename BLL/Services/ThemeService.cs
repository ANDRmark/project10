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
    class ThemeService : IThemeService
    {

        IUnitOfWork unitOfWork;
        IMapper mapper;
        public ThemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        void IThemeService.Delete(int id)
        {
            this.unitOfWork.Themes.Delete(id);
            this.unitOfWork.Save();
        }

        IEnumerable<ThemeDTO> IThemeService.GetAll()
        {
            IEnumerable<DAL.Models.Theme> themes = this.unitOfWork.Themes.GetAll();
            return this.mapper.Map<IEnumerable<ThemeDTO>>(themes);
        }

        ThemeDTO IThemeService.GetById(int id)
        {
            DAL.Models.Theme theme = this.unitOfWork.Themes.GetById(id);
            return this.mapper.Map<ThemeDTO>(theme);
        }

        void IThemeService.Insert(ThemeDTO newTheme)
        {
            DAL.Models.Theme theme = this.mapper.Map<DAL.Models.Theme>(newTheme);
            this.unitOfWork.Themes.Insert(theme);
            this.unitOfWork.Save();
        }

        void IThemeService.Update(ThemeDTO themeToUpdate)
        {
            DAL.Models.Theme theme = this.mapper.Map<DAL.Models.Theme>(themeToUpdate);
            this.unitOfWork.Themes.Update(theme);
            this.unitOfWork.Save();
        }
    }
}
