using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void Insert(T item);
        void Delete(int id);
        void Update(T item);



        //IEnumerable<T> GetAll();
        //IEnumerable<T> GetFiltered(Func<T, bool> predicat);
        //T GetById(int id);
        //void Insert(T item);
        //void Delete(int id);
        //void Update(T item);
    }
}
