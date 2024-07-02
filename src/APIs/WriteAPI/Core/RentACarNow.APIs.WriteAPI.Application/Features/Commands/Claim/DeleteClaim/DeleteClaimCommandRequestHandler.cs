﻿using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequestHandler : IRequestHandler<DeleteClaimCommandRequest, DeleteClaimCommandResponse>
    {
        public Task<DeleteClaimCommandResponse> Handle(DeleteClaimCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada talep silme işleminin kodunu yazmanız gerekecek
        }
    }

}
