namespace Product.Models
{
    public class PaginatedResult<T> : Result<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}