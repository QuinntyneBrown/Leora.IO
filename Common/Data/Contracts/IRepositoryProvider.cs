using System;
using System.Data.Entity;

namespace Common.Data.Contracts
{
    public interface IRepositoryProvider
    {
        DbContext dbContext { get; set; }

        IRepository<T> GetRepositoryForEntityType<T>() where T : class;

        T GetRepository<T>(Func<DbContext, object> factory = null) where T : class;

        void SetRepository<T>(T repository);
    }
}