using OurMemory.Data.Infrastructure;
using OurMemory.Domain.Entities;

namespace OurMemory.Data.Repositories
{
    public class VeteranRepository : Repository<Veteran>
    {

        public VeteranRepository(IDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }
    }
}