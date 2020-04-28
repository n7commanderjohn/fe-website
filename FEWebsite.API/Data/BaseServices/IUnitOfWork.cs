using System;
using System.Threading.Tasks;

namespace FEWebsite.API.Data.BaseServices
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveAllAsync();
    }
}
