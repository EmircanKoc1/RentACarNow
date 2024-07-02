using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.CreateCar
{
    public class CreateCarCommandRequestValidator : AbstractValidator<CreateCarCommandRequest>
    {
        public CreateCarCommandRequestValidator()
        {
            // Buraya marka güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
