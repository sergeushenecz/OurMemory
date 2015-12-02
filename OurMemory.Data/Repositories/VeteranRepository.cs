using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using OurMemory.Data.Infrastructure;
using OurMemory.Data.Interfaces;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Repositories
{
    public class VeteranRepository : RepositoryBase<Veteran>, IVeteranRepository
    {
        public void Add(Veteran entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Veteran entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Veteran entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<Veteran, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public Veteran GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public Veteran GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public Veteran Get(Expression<Func<Veteran, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veteran> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Veteran> GetMany(Expression<Func<Veteran, bool>> @where)
        {
            throw new NotImplementedException();
        }

        public VeteranRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }


}