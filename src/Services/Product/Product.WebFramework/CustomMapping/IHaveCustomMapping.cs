using AutoMapper;

namespace Product.WebFramework.CustomMapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
