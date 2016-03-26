using System;
using System.Linq.Expressions;

namespace OurMemory.Service.Specification.Core
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SatisfiedBy();

    }
}