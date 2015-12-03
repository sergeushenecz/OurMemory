namespace OurMemory.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        EntityDbContext Get(); 
    }
}