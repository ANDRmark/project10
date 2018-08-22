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
            return this.table.Include(m => m.User).Where(m => m.ThemeId == themeId).ToList();
        }

        IEnumerable<Message> IMessageRepository.Search(int themeId, string userName, string messageBody)
        {
            IQueryable<Message> query = this.table.Include(m => m.User).Where(m => m.ThemeId == themeId);
            if(userName != null)
            {
                query = query.Where(m => m.User.UserName == userName);
            }
            if(messageBody != null)
            {
                query = query.Where(m => m.MessageBody.ToLower().IndexOf(messageBody.ToLower()) > -1);
            }
            return query.ToList();
        }
    }
}
