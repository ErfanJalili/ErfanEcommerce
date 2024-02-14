using Product.Entities.Posts;
using Product.WebFramework.Api;

namespace Product.API.Models
{
    public class CategoryDto : BaseDto<CategoryDto, Category>
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public string ParentCategoryName { get; set; } //=> mapped from ParentCategory.Name
    }
}
