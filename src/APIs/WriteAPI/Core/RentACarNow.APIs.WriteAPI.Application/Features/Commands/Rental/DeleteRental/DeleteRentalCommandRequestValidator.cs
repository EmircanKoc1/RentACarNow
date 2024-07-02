using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequestValidator : AbstractValidator<DeleteRentalCommandRequest>
    {
        public DeleteRentalCommandRequestValidator()
        {
            // Buraya kiralama silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
