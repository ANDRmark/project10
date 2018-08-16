using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserInfoRepository:IRepository<UserInfo>
    {
        UserInfo GetByExternalId(string id);
        IEnumerable<UserInfo> SearchByUsername(string username);
    }
}
