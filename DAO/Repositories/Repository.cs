using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAO.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public abstract DbContext Context { get; }

        public abstract DbSet<T> Entities { get; }

        public virtual void Create(T entity)
        {
            Entities.Add(entity);
        }

        public virtual void Delete(int id)
        {
            Entities.Remove(GetById(id));
        }

        public virtual IQueryable<T> GetAll()
        {
            return Entities;
        }

        public virtual T GetById(int id)
        {
            return Entities.Find(id);
        }

        public virtual void Update(T entity)
        {
            Entities.Update(entity);
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}
