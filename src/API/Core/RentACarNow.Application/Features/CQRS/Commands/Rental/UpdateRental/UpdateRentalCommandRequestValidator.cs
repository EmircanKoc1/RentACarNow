using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequestValidator : AbstractValidator<UpdateRentalCommandRequest>
    {
        public UpdateRentalCommandRequestValidator()
        {
            // Buraya kiralama güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
