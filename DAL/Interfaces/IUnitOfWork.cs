using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Theme> Themes { get; }
        IRepository<Message> Messages { get; }
        void Save();
    }
}
