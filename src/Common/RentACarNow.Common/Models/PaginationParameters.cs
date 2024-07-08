namespace RentACarNow.Common.Models
{
    public record PaginationParameters
    {
        public PaginationParameters(int pageNumber, int size)
        {
            PageNumber = pageNumber;
            Size = size;
        }

        private readonly int maxPageSize = 100;
        private int size = 1;
        private int pageNumber = 1;

        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = value <= 0 ? 1 : value;

        }
        public int Size
        {
            get => size;
            set
            {
                if (value <= 0)
                    size = 1;
                else if (value > maxPageSize)
                    size = maxPageSize;
                else
                    size = value;
            }
        }


    }
}
