using OurMemory.Domain.Entities;
using OurMemory.Service.Specification.Core;

namespace OurMemory.Service.Specification
{
    class SpecificationBase<TEntity> where TEntity : DomainObject
    {
        public virtual Specification<TEntity> Empty()
        {
            return new Specification<TEntity>(x => true) { IsEmpty = true };
        }

        protected virtual Specification<TEntity> HasId(int? id)
        {
            return id.HasValue ? new Specification<TEntity>(x => x.Id == id)
                       : Empty();
        }

    }
}
