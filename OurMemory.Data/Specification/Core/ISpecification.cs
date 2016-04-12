using System;
using System.Linq.Expressions;

namespace OurMemory.Data.Specification.Core
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get;}
    }
}