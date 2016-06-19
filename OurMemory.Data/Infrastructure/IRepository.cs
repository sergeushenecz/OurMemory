using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Infrastructure
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long Id);
        T GetById(string Id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IQueryable<T> GetSpec(Expression<Func<T, bool>> specification);
        void DetachAllEntities();
        
    }
}