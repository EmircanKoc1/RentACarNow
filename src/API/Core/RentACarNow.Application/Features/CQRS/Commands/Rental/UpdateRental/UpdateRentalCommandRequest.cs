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

}
