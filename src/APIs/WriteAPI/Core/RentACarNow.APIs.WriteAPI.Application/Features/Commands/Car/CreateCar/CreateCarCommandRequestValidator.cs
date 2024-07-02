using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.CreateCar
{
    public class CreateCarCommandRequestValidator : AbstractValidator<CreateCarCommandRequest>
    {
        public CreateCarCommandRequestValidator()
        {
            // Buraya marka güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
