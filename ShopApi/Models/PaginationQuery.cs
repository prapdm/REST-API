namespace ShopApi.Models
{
    public class PaginationQuery
    {
        public string searchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
