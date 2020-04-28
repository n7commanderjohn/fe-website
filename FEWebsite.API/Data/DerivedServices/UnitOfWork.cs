using System;
using System.Threading.Tasks;
using FEWebsite.API.Core.Interfaces;

namespace FEWebsite.API.Data.DerivedServices
{
    public class UnitOfWork : BaseService, IUnitOfWork
    {
        public UnitOfWork(DataContext context) : base(context)
        {
        }

        public void Dispose()
        {
            this.Context.DisposeAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.Context
                .SaveChangesAsync()
                .ConfigureAwait(false) > 0;
        }
    }
}
