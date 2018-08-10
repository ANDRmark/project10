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
        protected DbContext context;
        protected DbSet<T> table;

        public Repository(DbContext context)
        {
            this.context = context;
            this.table = context.Set<T>();
        }

        public virtual void Delete(int id)
        {
            T entityToDelete = this.table.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(T item)
        {
            if (item == null) return;
            if (this.context.Entry(item).State == EntityState.Detached)
            {
                this.table.Attach(item);
            }
            this.table.Remove(item);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.table.ToList();
        }

        public virtual T GetById(int id)
        {
            return this.table.Find(id);
        }

        public virtual IEnumerable<T> GetFiltered(Expression<Func<T, bool>> predicat)
        {
            return this.table.Where(predicat).ToList();
        }

        public virtual void Insert(T item)
        {
            if (item == null) throw new ArgumentException();
            this.table.Add(item);
        }

        public virtual void Update(T item)
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
