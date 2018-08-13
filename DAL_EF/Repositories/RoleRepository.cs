using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using DAL_EF.EF;

namespace DAL_EF.Repositories
{
    class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ForumDbContext db) : base(db)
        {

        }

        Role IRoleRepository.GetByName(string rolename)
        {
            return this.table.Where(r => r.Name == rolename).FirstOrDefault();
        }
    }
}
