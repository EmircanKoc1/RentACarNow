using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Car.CreateCar
{
    public class CreateCarCommandRequest : IRequest<CreateCarCommandResponse>
    {
        // Buraya araç oluşturma için gerekli alanlar eklenebilir, örneğin marka, model, yıl gibi
    }

    public class CreateCarCommandResponse
    {
        // İsteğe bağlı olarak oluşturma sonucuyla ilgili bilgiler eklenebilir
    }

    public class CreateCarCommandRequestHandler : IRequestHandler<CreateCarCommandRequest, CreateCarCommandResponse>
    {
        public Task<CreateCarCommandResponse> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada araç oluşturma iş

        }
    }
}
