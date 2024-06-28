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
        // Buraya kiralama güncelleme için gerekli alanlar eklenebilir, örneğin rentalId, yeni bilgiler gibi
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
