namespace FEWebsite.API.Data.DerivedServices
{
    abstract public class BaseService
    {
        protected DataContext Context { get; }

        protected BaseService(DataContext context)
        {
            this.Context = context;
        }
    }
}
