using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DAL_EF.Repositories
{
    class UserInfoRepository: Repository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(ForumDbContext context):base(context)
        {

        }

        public override IEnumerable<UserInfo> GetAll()
        {
            return this.table.Include(u => u.Roles);
        }
        public override UserInfo GetById(int id)
        {
            return this.table.Include(u => u.Roles).FirstOrDefault(u => u.Id == id);
        }

        public override IEnumerable<UserInfo> GetFiltered(Expression<Func<UserInfo, bool>> predicat)
        {
            return this.table.Include(u => u.Roles).Where(predicat).ToList();
        }

        UserInfo IUserInfoRepository.GetByExternalId(string id)
        {
            return this.table.Include(u => u.Roles).Where(u => u.ExternalUserId == id).FirstOrDefault();
        }

        IEnumerable<UserInfo> IUserInfoRepository.SearchByUsername(string username)
        {
            return this.table.Include(u => u.Roles).Where(u => u.UserName.ToLower().IndexOf(username.ToLower()) > -1);
        }

        public override void Update(UserInfo item)
        {
            if (item == null) throw new ArgumentException();

            UserInfo original = this.table.Find(item.Id);
            if (original != null)
            {
                this.context.Entry(original).CurrentValues.SetValues(item);

                original.Roles = item.Roles.Select(r => this.context.Set<Role>().Find(r.Id)).ToList();
                //original.Roles = item.Roles;
            }
        }
    }
}
