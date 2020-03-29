using HotelManagementSystem.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Core.Service
{
    public abstract class SaveableCollection<T> where T : class
    {
        protected IRepository<T> Repository;

        protected SaveableCollection(IRepository<T> repository)
        {
            Repository = repository;
        }

        public void Save()
        {
            Repository.Save();
        }

        public void Add(T entity)
        {
            Repository.Add(entity);
        }

        public void Remove(T entity)
        {
            Repository.Remove(entity);
        }

        public T Find(Expression<Func<T,bool>> predicate)
        {
            return Repository.Find(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return Repository.FindAll(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return Repository.GetAll();
        }

        public T GetById(int id)
        {
            return Repository.Get(id);
        }
    }
}