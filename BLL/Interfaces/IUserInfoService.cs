using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserInfoService
    {
        IEnumerable<UserInfoDTO> GetAll();
        UserInfoDTO GetById(int id);
        UserInfoDTO GetByExternalId(string id);
        UserInfoDTO GetByUsername(string username);
        IEnumerable<UserInfoDTO> SearchByUsernamePart(string username);
        void Insert(UserInfoDTO newUserInfo);
        void Update(UserInfoDTO userInfp);
        void Delete(int id);
    }
}
