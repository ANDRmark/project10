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
        //private ForumDbContext context;

        //public MessageRepository(ForumDbContext context)
        //{
        //    this.context = context;
        //}

        //void IRepository<Message>.Delete(int id)
        //{
        //    Message message = this.context.Messages.Find(id);
        //    if(message != null)
        //    {
        //        this.context.Messages.Remove(message);
        //    }
        //}

        //IEnumerable<Message> IRepository<Message>.GetAll()
        //{
        //    return this.context.Messages.ToList();
        //}

        //IEnumerable<Message> IRepository<Message>.GetFiltered(Expression<Func<Message, bool>> predicate)
        //{
        //    return this.context.Messages.Where(predicate).ToList();
        //}

        //Message IRepository<Message>.GetById(int id)
        //{
        //    return this.context.Messages.Find(id);
        //}

        //void IRepository<Message>.Insert(Message message)
        //{
        //    if (message == null)
        //        throw new ArgumentNullException(nameof(message));

        //    var check = this.context.Messages.Find(message.Id);
        //    if (check == null)
        //        this.context.Messages.Add(message);
        //}

        //void IRepository<Message>.Update(Message message)
        //{
        //    if (message == null) throw new ArgumentException(nameof(message));

        //    Message original = this.context.Messages.Find(message.Id);
        //    if (original != null)
        //    {
        //        this.context.Entry(original).CurrentValues.SetValues(message);
        //    }
        //}
    }
}
