using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.UpdateCar
{
    public class UpdateCarCommandRequestValidator : AbstractValidator<UpdateCarCommandRequest>
    {
        public UpdateCarCommandRequestValidator()
        {
            // Buraya araç güncelleme komutunun doğrulama kurallarını ekleye

        }

    }
}