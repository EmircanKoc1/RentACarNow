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
        public Guid Id { get; set; }

    }

}
