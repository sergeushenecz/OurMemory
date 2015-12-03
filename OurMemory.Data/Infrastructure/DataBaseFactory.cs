namespace OurMemory.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private ApplicationDbContext _dataContext;
        public ApplicationDbContext Get()
        {
            return _dataContext ?? (_dataContext = new ApplicationDbContext());
        }
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}