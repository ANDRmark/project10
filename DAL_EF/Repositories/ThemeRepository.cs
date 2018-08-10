using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.Repositories
{
    class ThemeRepository : Repository<Theme> , IThemeRepository
    {

        public ThemeRepository(ForumDbContext context):base(context)
        {

        }

        //void IRepository<Theme>.Delete(int id)
        //{
        //    Message theme = this.context.Messages.Find(id);
        //    if (theme != null)
        //    {
        //        this.context.Messages.Remove(theme);
        //    }
        //}

        //IEnumerable<Theme> IRepository<Theme>.GetAll()
        //{
        //    return this.context.Themes.ToList();
        //}

        //IEnumerable<Theme> IRepository<Theme>.GetFiltered(Expression<Func<Theme, bool>> predicate)
        //{
        //    return this.context.Themes.Where(predicate).ToList();
        //}

        //Theme IRepository<Theme>.GetById(int id)
        //{
        //    return this.context.Themes.Find(id);
        //}

        //void IRepository<Theme>.Insert(Theme theme)
        //{
        //    if (theme == null)
        //        throw new ArgumentNullException(nameof(theme));

        //    var check = this.context.Themes.Find(theme.Id);
        //    if (check == null)
        //        this.context.Themes.Add(theme);
        //}

        //void IRepository<Theme>.Update(Theme theme)
        //{
        //    if (theme == null) throw new ArgumentException(nameof(theme));

        //    Theme original = this.context.Themes.Find(theme.Id);
        //    if (original != null)
        //    {
        //        this.context.Entry(original).CurrentValues.SetValues(theme);
        //    }
        //}
    }
}
