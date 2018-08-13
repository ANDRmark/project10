using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetAll();
        RoleDTO GetById(int id);
        RoleDTO GetByName(string rolename);
        void Insert(RoleDTO newRole);
        void Update(RoleDTO role);
        void Delete(int id);
    }
}
