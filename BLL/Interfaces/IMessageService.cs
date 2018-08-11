using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMessageService
    {
        IEnumerable<MessageDTO> GetAll();
        MessageDTO GetById(int id);
        void Insert(MessageDTO newMessage);
        void Update(MessageDTO message);
        IEnumerable<MessageDTO> GetByThemeId(int messageId);
        void Delete(int id);
    }
}
