using FluentValidation;
using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequest : IRequest<DeleteRentalCommandResponse>
    {
        public Guid Id { get; set; }

    }

    public class DeleteRentalCommandResponse
    {
        // İsteğe bağlı olarak silme sonucuyla ilgili bilgiler eklenebilir
    }

    public class DeleteRentalCommandRequestHandler : IRequestHandler<DeleteRentalCommandRequest, DeleteRentalCommandResponse>
    {
        public Task<DeleteRentalCommandResponse> Handle(DeleteRentalCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada kiralama silme işleminin kodunu yazmanız gerekecek
        }
    }

    public class DeleteRentalCommandRequestValidator : AbstractValidator<DeleteRentalCommandRequest>
    {
        public DeleteRentalCommandRequestValidator()
        {
            // Buraya kiralama silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
