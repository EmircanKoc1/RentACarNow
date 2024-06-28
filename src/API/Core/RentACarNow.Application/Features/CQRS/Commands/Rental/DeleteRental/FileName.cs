using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.DeleteRental
{
    public class DeleteRentalCommandRequest : IRequest<DeleteRentalCommandResponse>
    {
        // Buraya kiralama silme için gerekli alanlar eklenebilir, örneğin rentalId gibi
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
