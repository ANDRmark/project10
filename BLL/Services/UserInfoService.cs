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
    class UserInfoService: IUserInfoService
    {

        IUnitOfWork unitOfWork;
        IMapper mapper;
        public UserInfoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        void IUserInfoService.Delete(int id)
        {
            this.unitOfWork.Users.Delete(id);
            this.unitOfWork.Save();
        }

        IEnumerable<UserInfoDTO> IUserInfoService.GetAll()
        {
            IEnumerable<DAL.Models.UserInfo> users = this.unitOfWork.Users.GetAll();
            return this.mapper.Map<IEnumerable<UserInfoDTO>>(users);
        }

        UserInfoDTO IUserInfoService.GetByExternalId(string id)
        {
            DAL.Models.UserInfo user = this.unitOfWork.Users.GetByExternalId(id);
            return this.mapper.Map<UserInfoDTO>(user);
        }

        UserInfoDTO IUserInfoService.GetById(int id)
        {
            DAL.Models.UserInfo user = this.unitOfWork.Users.GetById(id);
            return this.mapper.Map<UserInfoDTO>(user);
        }

        UserInfoDTO IUserInfoService.GetByUsername(string username)
        {
            DAL.Models.UserInfo user = this.unitOfWork.Users.SearchByUsername(username).FirstOrDefault();
            return this.mapper.Map<UserInfoDTO>(user);
        }
        IEnumerable<UserInfoDTO> IUserInfoService.SearchByUsername(string username)
        {
            IEnumerable<DAL.Models.UserInfo> user = this.unitOfWork.Users.SearchByUsername(username);
            return this.mapper.Map< IEnumerable<UserInfoDTO>>(user);
        }

        void IUserInfoService.Insert(UserInfoDTO newUserInfo)
        {
            DAL.Models.UserInfo user = this.mapper.Map<DAL.Models.UserInfo>(newUserInfo);
            this.unitOfWork.Users.Insert(user);
            this.unitOfWork.Save();
        }

        void IUserInfoService.Update(UserInfoDTO userToUpdate)
        {
            DAL.Models.UserInfo user = this.mapper.Map<DAL.Models.UserInfo>(userToUpdate);
            this.unitOfWork.Users.Update(user);
            this.unitOfWork.Save();
        }
    }
}
