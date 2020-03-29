using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HotelManagementSystem.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity) => dbSet.Add(entity);

        public void AddRange(IEnumerable<T> entities) => dbSet.AddRange(entities);

        public T Find(Expression<Func<T, bool>> predicate) => dbSet.FirstOrDefault(predicate);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate) => dbSet.Where(predicate);

        public T Get(int id) => dbSet.Find(id);

        public IEnumerable<T> GetAll() => dbSet.ToList();

        public void Remove(T entity) => dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => dbSet.RemoveRange(entities);

        public void Save()
        {
            context.SaveChanges();
        }
    }
}