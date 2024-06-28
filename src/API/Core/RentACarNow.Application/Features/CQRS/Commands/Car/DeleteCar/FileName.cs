using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequest : IRequest<DeleteCarCommandResponse>
    {
        // Buraya araç silme için gerekli alanlar eklenebilir, örneğin carId gibi
    }

    public class DeleteCarCommandResponse
    {
        // İsteğe bağlı olarak silme sonucuyla ilgili bilgiler eklenebilir
    }

    public class DeleteCarCommandRequestHandler : IRequestHandler<DeleteCarCommandRequest, DeleteCarCommandResponse>
    {
        public Task<DeleteCarCommandResponse> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada araç silme işleminin kodunu yazmanız gerekecek
        }
    }

    public class DeleteCarCommandRequestValidator : AbstractValidator<DeleteCarCommandRequest>
    {
        public DeleteCarCommandRequestValidator()
        {
            // Buraya araç silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
