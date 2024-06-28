using FluentValidation;
using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.UpdateCar
{
    public class UpdateCarCommandRequest : IRequest<UpdateCarCommandResponse>
    {
        public Guid Id { get; set; }

    }

    public class UpdateCarCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateCarCommandRequestHandler : IRequestHandler<UpdateCarCommandRequest, UpdateCarCommandResponse>
    {
        public Task<UpdateCarCommandResponse> Handle(UpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada araç güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateCarCommandRequestValidator : AbstractValidator<UpdateCarCommandRequest>
    {
        public UpdateCarCommandRequestValidator()
        {
            // Buraya araç güncelleme komutunun doğrulama kurallarını ekleye

        }

    }
}