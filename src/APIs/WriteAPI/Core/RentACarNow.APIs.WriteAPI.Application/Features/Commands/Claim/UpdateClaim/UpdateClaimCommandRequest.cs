﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequest : IRequest<UpdateClaimCommandResponse>
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

    }

}
