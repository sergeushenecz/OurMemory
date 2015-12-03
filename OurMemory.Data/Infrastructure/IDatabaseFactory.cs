namespace OurMemory.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        ApplicationDbContext Get(); 
    }
}