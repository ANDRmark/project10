using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        IEnumerable<Message> GetMessagesByThemeIdWithUsers(int themeId);
        IEnumerable<Message> Search(int themeId, string userName, string messageBody);
    }
}
