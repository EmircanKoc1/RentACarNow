using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequestValidator : AbstractValidator<DeleteRentalCommandRequest>
    {
        public DeleteRentalCommandRequestValidator()
        {
            // Buraya kiralama silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
