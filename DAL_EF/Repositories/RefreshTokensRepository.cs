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
    class RefreshTokensRepository: Repository<RefreshToken> , IRefreshTokensRepository
    {
        public RefreshTokensRepository(ForumDbContext context):base(context)
        {
               
        }
    }
}
