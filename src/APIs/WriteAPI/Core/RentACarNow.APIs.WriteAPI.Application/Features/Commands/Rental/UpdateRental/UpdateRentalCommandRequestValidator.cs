using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequestValidator : AbstractValidator<UpdateRentalCommandRequest>
    {
        public UpdateRentalCommandRequestValidator()
        {
            // Buraya kiralama güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
