using AutoMapper;
using Product.API.Models;
using Product.Entities.Posts;
using Product.Infrastructure.Contracts;
using Product.WebFramework.Api;

namespace Product.API.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoriesController : CrudController<CategoryDto, Category>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoriesController(IRepository<Category> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
