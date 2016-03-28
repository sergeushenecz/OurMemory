using OurMemory.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace OurMemory.Service.Specification.Core
{
    public class Specification<TEntity> : ISpecification<TEntity> where TEntity : DomainObject
    {
        public bool IsEmpty { get; set; }
        public Expression<Func<TEntity, bool>> Predicate { get; private set; }

        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            IsEmpty = false;
            Predicate = predicate;
        }

        public bool IsSatisfiedBy(TEntity item)
        {
            return Predicate.Compile().Invoke(item);
        }


        public Specification<TEntity> And(Specification<TEntity> other)
        {
            return Combine(ExpressionType.AndAlso, this, other);
        }

        public Specification<TEntity> Or(Specification<TEntity> other)
        {
            return Combine(ExpressionType.OrElse, this, other);
        }

        public static Specification<TEntity> operator |(Specification<TEntity> left, Specification<TEntity> right)
        {
            return left.Or(right);
        }

        public static Specification<TEntity> operator &(Specification<TEntity> left, Specification<TEntity> right)
        {
            return right.IsEmpty ? left : left.And(right);
        }
        public static Specification<TEntity> operator !(Specification<TEntity> operand)
        {
            var expression = Expression.Not(operand.Predicate.Body);

            var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, operand.Predicate.Parameters);

            return new Specification<TEntity>(lambda);
        }

        private static Specification<TEntity> Combine(ExpressionType type, Specification<TEntity> left, Specification<TEntity> right)
        {
            InvocationExpression rightInvoke = Expression.Invoke(right.Predicate, left.Predicate.Parameters);

            BinaryExpression expression = Expression.MakeBinary(type, left.Predicate.Body, rightInvoke);

            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(expression, left.Predicate.Parameters);

            return new Specification<TEntity>(lambda);
        }
    }
}
