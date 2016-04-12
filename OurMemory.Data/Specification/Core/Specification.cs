using System;
using System.Linq;
using System.Linq.Expressions;
using Neptune.DataAccess.Specification.Utils;

namespace OurMemory.Data.Specification.Core
{
    public class Specification<TEntity> : ISpecification<TEntity> where TEntity : class 
    {
        public bool IsEmpty { get; set; }
        public Expression<Func<TEntity, bool>> Predicate { get; private set; }

        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            IsEmpty = false;
            Predicate = EntityCastRemoverVisitor.Convert(predicate);
        }

        public bool IsSatisfiedBy(TEntity item)
        {
            return Predicate.Compile().Invoke(item);
        }


        public Specification<TEntity> And(Specification<TEntity> other)
        {
            return Compose(this, other, Expression.And);
        }

        public Specification<TEntity> Or(Specification<TEntity> other)
        {
            return Compose(this, other, Expression.Or);
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

        private static Specification<TEntity> Compose(Specification<TEntity> left, Specification<TEntity> right, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = left.Predicate.Parameters.Select((f, i) => new { f, s = right.Predicate.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, right.Predicate.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return new Specification<TEntity>(Expression.Lambda<Func<TEntity, bool>>(merge(left.Predicate.Body, secondBody), left.Predicate.Parameters));
        }



        private sealed class EntityCastRemoverVisitor : ExpressionVisitor
        {
            public static Expression<Func<TEntity, bool>> Convert<TEntity>(
                Expression<Func<TEntity, bool>> predicate)
            {
                var visitor = new EntityCastRemoverVisitor();

                var visitedExpression = visitor.Visit(predicate);

                return (Expression<Func<TEntity, bool>>)visitedExpression;
            }

            protected override Expression VisitUnary(UnaryExpression node)
            {
                if (node.NodeType == ExpressionType.Convert)
                {
                    return node.Operand;
                }

                return base.VisitUnary(node);
            }
        }
    }



}
