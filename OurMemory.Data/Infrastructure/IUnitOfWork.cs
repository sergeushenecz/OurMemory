namespace OurMemory.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit(); 
    }
}