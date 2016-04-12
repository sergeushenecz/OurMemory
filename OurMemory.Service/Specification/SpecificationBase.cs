using OurMemory.Data.Specification.Core;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Specification
{
    public class SpecificationBase<TEntity> where TEntity : DomainObject
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

        protected virtual Specification<TEntity> IsDeleted()
        {
            return new Specification<TEntity>(x => x.IsDeleted);
        }

    }
}
