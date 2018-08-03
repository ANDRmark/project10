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
    class MessageService : IMessageService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        void IMessageService.Delete(int id)
        {
            this.unitOfWork.Messages.Delete(id);
            this.unitOfWork.Save();
        }

        IEnumerable<MessageDTO> IMessageService.GetAll()
        {
            throw new NotImplementedException();
        }

        MessageDTO IMessageService.GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IMessageService.Insert(MessageDTO newCategory)
        {
            throw new NotImplementedException();
        }

        void IMessageService.Update(MessageDTO category)
        {
            throw new NotImplementedException();
        }
    }
}
