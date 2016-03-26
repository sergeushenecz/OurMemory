using System;
using System.Linq.Expressions;
using OurMemory.Service.Specification.Extention;

namespace OurMemory.Service.Specification.Core
{
    public class OrSpecification<T> : Specification<T> where T : class
    {
        ISpecification<T> leftSpecification;
        ISpecification<T> rightSpecification;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.leftSpecification = left;
            this.rightSpecification = right;
        }

        public ISpecification<T> LeftSpecification
        {
            get { return leftSpecification; }
        }

        public ISpecification<T> RightSpecification
        {
            get { return rightSpecification; }
        }

        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            Expression<Func<T, bool>> left = LeftSpecification.SatisfiedBy();
            Expression<Func<T, bool>> right = RightSpecification.SatisfiedBy();

            return left.Or(right);
        }
    }
}