namespace Emerce_API.Infrastructure
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int maxPageSize { get; } = 15;

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = maxPageSize;

        }
        public PaginationFilter( int pageNumber, int pageSize )
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > maxPageSize ? maxPageSize : pageSize;
        }
    }
}
