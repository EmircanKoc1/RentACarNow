using FluentValidation;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll
{
    public class GetAllRentalQueryRequestValidator : AbstractValidator<GetAllRentalQueryRequest>
    {
        public GetAllRentalQueryRequestValidator()
        {
            // Burada Rental ile ilgili sorgu komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
