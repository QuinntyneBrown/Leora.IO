using System;
namespace Common.Data.Contracts
{
    public interface IUow : IDisposable
    {
        void SaveChanges();
    }
}
