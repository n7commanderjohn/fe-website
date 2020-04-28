using System;
using System.Threading.Tasks;

namespace FEWebsite.API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAllAsync();
    }
}
