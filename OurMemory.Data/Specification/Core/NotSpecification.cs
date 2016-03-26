using System;
using System.Linq.Expressions;
using OurMemory.Service.Specification.Extention;

namespace OurMemory.Service.Specification.Core
{
    public class NotSpecification<T> : Specification<T> where T : class
    {
        readonly ISpecification<T> specification;

        public NotSpecification(ISpecification<T> specification)
        {
            this.specification = specification;
        }

        public ISpecification<T> LeftSpecification
        {
            get { return specification; }
        }


        public override Expression<Func<T, bool>> SatisfiedBy()
        {
            Expression<Func<T, bool>> specification = LeftSpecification.SatisfiedBy();

            return specification.Not();
        }
    }
}