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
    class RoleService : IRoleService
    {
        IUnitOfWork unitOfWork;
        IMapper mapper;
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        void IRoleService.Delete(int id)
        {
            this.unitOfWork.Roles.Delete(id);
            this.unitOfWork.Save();
        }

        IEnumerable<RoleDTO> IRoleService.GetAll()
        {
            IEnumerable<DAL.Models.Role> roles = this.unitOfWork.Roles.GetAll();
            return this.mapper.Map<IEnumerable<RoleDTO>>(roles);
        }

        RoleDTO IRoleService.GetById(int id)
        {
            DAL.Models.Role role = this.unitOfWork.Roles.GetById(id);
            return this.mapper.Map<RoleDTO>(role);
        }

        RoleDTO IRoleService.GetByName(string rolename)
        {
            DAL.Models.Role role = this.unitOfWork.Roles.GetByName(rolename);
            return this.mapper.Map<RoleDTO>(role);
        }

        void IRoleService.Insert(RoleDTO newRole)
        {
            DAL.Models.Role role = this.mapper.Map<DAL.Models.Role>(newRole);
            this.unitOfWork.Roles.Insert(role);
            this.unitOfWork.Save();
        }

        void IRoleService.Update(RoleDTO roleToUpdate)
        {
            DAL.Models.Role role = this.mapper.Map<DAL.Models.Role>(roleToUpdate);
            this.unitOfWork.Roles.Update(role);
            this.unitOfWork.Save();
        }
    }
}
