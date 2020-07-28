using System;
using System.Threading.Tasks;

namespace FEWebsite.API.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAllAsync();
    }
}
