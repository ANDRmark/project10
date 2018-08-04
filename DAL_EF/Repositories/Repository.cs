using DAL.Interfaces;
using DAL_EF.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL_EF.Repositories
{
    class Repository<T> : IRepository<T> where T : DAL.Models.Entity
    {
        ForumDbContext context;
        DbSet<T> table;

        public Repository(ForumDbContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }

        void IRepository<T>.Delete(int id)
        {
            T entityToDelete = this.table.Find(id);
            this.Delete(entityToDelete);
        }

        public void Delete(T item)
        {
            if (item == null) return;
            if (this.context.Entry(item).State == EntityState.Detached)
            {
                this.table.Attach(item);
            }
            this.table.Remove(item);
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            return this.table.ToList();
        }

        T IRepository<T>.GetById(int id)
        {
            return this.table.Find(id);
        }

        IEnumerable<T> IRepository<T>.GetFiltered(Expression<Func<T, bool>> predicat)
        {
            return this.table.Where(predicat).ToList();
        }

        void IRepository<T>.Insert(T item)
        {
            if (item == null) throw new ArgumentException();
            this.table.Add(item);
        }

        void IRepository<T>.Update(T item)
        {
            if (item == null) throw new ArgumentException();

            T original = this.table.Find(item.Id);
            if (original != null)
            {
                this.context.Entry(original).CurrentValues.SetValues(item);
            }
        }
    }
}
