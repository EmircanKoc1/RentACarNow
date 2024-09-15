namespace RentACarNow.Common.Models
{
    public class PaginationInfo
    {
        public int PageNumber { get; set; }
        public int PageItemSize { get; set; }
        public long TotalItemCount { get; set; }
        public long TotalPageCount => (long)Math.Ceiling((double)TotalItemCount / PageItemSize);

    }
}
