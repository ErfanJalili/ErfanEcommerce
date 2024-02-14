using Common;

namespace Product.Services.DataInitializer
{
    public interface IDataInitializer : IScopedDependency
    {
        void InitializeData();
    }
}
