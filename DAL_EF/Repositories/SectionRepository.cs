using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DAL_EF.Repositories
{
    class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(ForumDbContext context):base(context)
        {

        }

        Section ISectionRepository.GetByIdWithThemes(int id)
        {
            return this.table.Include(s => s.Themes).FirstOrDefault(s => s.Id == id);
        }

        IEnumerable<Section> ISectionRepository.SearchByNamePart(string sectionNamePart)
        {
            return this.table.Where(s => s.Title.ToLower().IndexOf(sectionNamePart.ToLower()) > -1);
        }
    }
}
