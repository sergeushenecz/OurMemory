using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OurMemory.Domain.Entities;
using OurMemory.Service.Specification.Core;

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
        IEnumerable<T> GetSpec(Expression<Func<T, bool>> specification);

    }
}