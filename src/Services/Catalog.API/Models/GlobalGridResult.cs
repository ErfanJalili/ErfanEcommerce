namespace Catalog.API.Models
{
    public class GlobalGridResult<T> where T : class
    {
        public IEnumerable<T>? Data { get; set; }
        public int TotalCount { get; set; }
    }
}
