namespace RentACarNow.Common.Models
{
    public class PaginationInfo
    {
        public int PageNumber { get; set; }
        public int PageItemSize { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPageCount => (int)Math.Ceiling((double)TotalItemCount / PageItemSize);

    }
}
