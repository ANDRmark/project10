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
        void Insert(MessageDTO newCategory);
        void Update(MessageDTO category);
        void Delete(int id);
    }
}
