using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.UpdateRental
{
    public class UpdateRentalCommandRequest : IRequest<UpdateRentalCommandResponse>
    {
        public Guid Id { get; set; }

    }

    public class UpdateRentalCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateRentalCommandRequestHandler : IRequestHandler<UpdateRentalCommandRequest, UpdateRentalCommandResponse>
    {
        public Task<UpdateRentalCommandResponse> Handle(UpdateRentalCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada kiralama güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateRentalCommandRequestValidator : AbstractValidator<UpdateRentalCommandRequest>
    {
        public UpdateRentalCommandRequestValidator()
        {
            // Buraya kiralama güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
