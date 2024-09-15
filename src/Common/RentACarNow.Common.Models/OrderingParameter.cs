namespace RentACarNow.Common.Models
{
    public class OrderingParameter
    {
        public bool Sort { get; set; }
        public bool IsAscending { get; set; }
        public string SortingField { get; set; }


        public OrderingParameter()
        {

        }

        public OrderingParameter(bool sort, bool isAscending, string sortingField)
        {
            Sort = sort;
            IsAscending = isAscending;
            SortingField = sortingField;
        }

        public static OrderingParameter CreateOrderingParameter(bool sort, bool isAscending, string sortingField)
            => new OrderingParameter(sort, isAscending, sortingField);



    }
}
