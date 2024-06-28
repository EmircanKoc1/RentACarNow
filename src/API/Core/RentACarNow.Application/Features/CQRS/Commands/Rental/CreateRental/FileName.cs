using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.CreateRental
{
    public class CreateRentalCommandRequest : IRequest<CreateRentalCommandResponse>
    {
        // Buraya kiralama oluşturma için gerekli alanlar eklenebilir, örneğin rentalId, yeni bilgiler gibi
    }

    public class CreateRentalCommandResponse
    {
        // İsteğe bağlı olarak oluşturma sonucuyla ilgili bilgiler eklenebilir
    }

    public class CreateRentalCommandRequestHandler : IRequestHandler<CreateRentalCommandRequest, CreateRentalCommandResponse>
    {
        public Task<CreateRentalCommandResponse> Handle(CreateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada kiralama oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

    public class CreateRentalCommandRequestValidator : AbstractValidator<CreateRentalCommandRequest>
    {
        public CreateRentalCommandRequestValidator()
        {
            // Buraya kiralama oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
