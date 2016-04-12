using System.Linq;

namespace OurMemory.Service.Extenshions
{
    public static class QueryableExtentions
    {
        public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int skip, int pageSize)
        {
            return query.Skip(skip).Take(pageSize);
        }
    }
}