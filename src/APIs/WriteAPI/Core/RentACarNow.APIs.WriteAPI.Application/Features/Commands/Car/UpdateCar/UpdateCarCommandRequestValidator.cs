using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.UpdateCar
{
    public class UpdateCarCommandRequestValidator : AbstractValidator<UpdateCarCommandRequest>
    {
        public UpdateCarCommandRequestValidator()
        {
            // Buraya araç güncelleme komutunun doğrulama kurallarını ekleye

        }

    }
}