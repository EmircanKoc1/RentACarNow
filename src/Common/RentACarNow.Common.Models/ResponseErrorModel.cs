namespace RentACarNow.Common.Models
{
    public class ResponseErrorModel
    {
        public string? PropertyName { get; set; } = null!;
        //public IEnumerable<string> ErrorMessages { get; set; } = null!;
        public string ErrorMessage { get; set; }
    }
}
