﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Car.DeleteCar
{
    public class DeleteCarCommandRequest : IRequest<DeleteCarCommandResponse>
    {
        public Guid CarId { get; set; }

    }

}
