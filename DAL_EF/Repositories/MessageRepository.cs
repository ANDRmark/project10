using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.Repositories
{
    class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ForumDbContext db):base(db)
        {

        }

        IEnumerable<Message> IMessageRepository.GetMessagesByThemeIdWithUsers(int themeId)
        {
            return this.table.Where(m => m.ThemeId == themeId).Include(m => m.User).ToList();
        }
    }
}
