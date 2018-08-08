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
            IEnumerable<DAL.Models.Message> messages = this.unitOfWork.Messages.GetAll();
            return this.mapper.Map<IEnumerable<MessageDTO>>(messages);
        }

        MessageDTO IMessageService.GetById(int id)
        {
            DAL.Models.Message message = this.unitOfWork.Messages.GetById(id);
            return this.mapper.Map<MessageDTO>(message);
        }

        void IMessageService.Insert(MessageDTO newMessage)
        {
            DAL.Models.Message message = this.mapper.Map<DAL.Models.Message>(newMessage);
            this.unitOfWork.Messages.Insert(message);
            this.unitOfWork.Save();
        }

        void IMessageService.Update(MessageDTO messageToUpdate)
        {
            DAL.Models.Message message = this.mapper.Map<DAL.Models.Message>(messageToUpdate);
            this.unitOfWork.Messages.Update(message);
            this.unitOfWork.Save();
        }
        IEnumerable<MessageDTO> IMessageService.GetByThemeId(int themeid)
        {
            IEnumerable<DAL.Models.Message> messages = this.unitOfWork.Messages.GetFiltered(m => m.ThemeId == themeid);
            return this.mapper.Map<IEnumerable<MessageDTO>>(messages);
        }
    }
}
