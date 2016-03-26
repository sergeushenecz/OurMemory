using System;
using System.Linq.Expressions;

namespace OurMemory.Service.Specification.Core
{
    public abstract class Specification<TEntity> : ISpecification<TEntity> where TEntity : class
    {
        public abstract Expression<Func<TEntity, bool>> SatisfiedBy();

        public static ISpecification<TEntity> And(ISpecification<TEntity> left, ISpecification<TEntity> right)
        {
            return new AndSpecification<TEntity>(left, right);
        }

        public static ISpecification<TEntity> Or(Specification<TEntity> left, Specification<TEntity> right)
        {
            return new OrSpecification<TEntity>(left, right);
        }

        public static ISpecification<TEntity> Not(Specification<TEntity> specification)
        {
            return new NotSpecification<TEntity>(specification);
        }
    }
}
