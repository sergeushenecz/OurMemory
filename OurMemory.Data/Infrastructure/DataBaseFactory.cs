namespace OurMemory.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private EntityDbContext _dataContext;
        public EntityDbContext Get()
        {
            return _dataContext ?? (_dataContext = new EntityDbContext());
        }
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}