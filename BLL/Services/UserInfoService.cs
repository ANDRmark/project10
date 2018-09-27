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


        UserInfoDTO IUserInfoService.GetById(int id)
        {
            DAL.Models.UserInfo user = this.unitOfWork.Users.GetById(id);
            return this.mapper.Map<UserInfoDTO>(user);
        }

        UserInfoDTO IUserInfoService.GetByUsername(string username)
        {
            DAL.Models.UserInfo user = this.unitOfWork.Users.GetByUsername(username);
            return this.mapper.Map<UserInfoDTO>(user);
        }
        IEnumerable<UserInfoDTO> IUserInfoService.SearchByUsernamePart(string usernamePart)
        {
            IEnumerable<DAL.Models.UserInfo> user = this.unitOfWork.Users.SearchByUsernamePart(usernamePart);
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
            //DAL.Models.UserInfo user = this.unitOfWork.Users.GetById(userToUpdate.Id);
            var user = new DAL.Models.UserInfo();
            user = this.mapper.Map(userToUpdate, user);
            user.Roles = userToUpdate.Roles.Select(r => new DAL.Models.Role() { Name=r.Name}).ToList();
            //user.Roles = userToUpdate.Roles.Select(r => this.unitOfWork.Roles.GetByName(r.Name)).Where(r => r != null).ToList();
            this.unitOfWork.Users.Update(user);
            this.unitOfWork.Save();


            //DAL.Models.UserInfo user = this.mapper.Map<DAL.Models.UserInfo>(userToUpdate);
            //user.PasswordHash = this.unitOfWork.Users.GetById(user.Id).PasswordHash;
            //this.unitOfWork.Users.Update(user);
            //this.unitOfWork.Save();
        }
    }
}
