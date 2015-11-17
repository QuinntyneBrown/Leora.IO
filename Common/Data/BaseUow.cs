using System;
using System.Data.Entity;
using Common.Data.Contracts;

namespace Common.Data
{
    public class BaseUow : IUow
    {
        protected DbContext dbContext;

        protected IRepositoryProvider RepositoryProvider { get; set; }

        public BaseUow(DbContext dbContext = null)
        {
            this.dbContext = dbContext;

            ConfigureDbContext(this.dbContext);

            var repositoryProvider = new RepositoryProvider(new RepositoryFactories());

            repositoryProvider.dbContext = this.dbContext;

            RepositoryProvider = repositoryProvider;
        }

        public BaseUow(IRepositoryProvider repositoryProvider, DbContext dbContext = null)
        {
            this.dbContext = dbContext;

            ConfigureDbContext(this.dbContext);

            repositoryProvider.dbContext = this.dbContext;

            RepositoryProvider = repositoryProvider;
        }

        protected void ConfigureDbContext(DbContext dbContext)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.ValidateOnSaveEnabled = false;
        }


        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        protected IRepository<T> GetStandardRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepositoryForEntityType<T>();
        }

        protected T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dbContext != null)
                {
                    this.dbContext.Dispose();
                }
            }
        }

        #endregion


        public void Add<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
