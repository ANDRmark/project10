using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.Repositories
{
    class UserInfoRepository: Repository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(ForumDbContext context):base(context)
        {

        }

        UserInfo IUserInfoRepository.GetByExternalId(string id)
        {
            return this.table.Where(u => u.ExternalUserId == id).FirstOrDefault();
        }

        UserInfo IUserInfoRepository.GetByUsername(string username)
        {
            return this.table.Where(u => u.UserName == username).FirstOrDefault();
        }
    }
}
