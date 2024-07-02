using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequestValidator : AbstractValidator<DeleteCarCommandRequest>
    {
        public DeleteCarCommandRequestValidator()
        {
            // Buraya araç silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
