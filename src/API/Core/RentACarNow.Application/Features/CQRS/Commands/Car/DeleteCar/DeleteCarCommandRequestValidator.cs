using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequestValidator : AbstractValidator<DeleteCarCommandRequest>
    {
        public DeleteCarCommandRequestValidator()
        {
            // Buraya araç silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
