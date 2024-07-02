using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequestHandler : IRequestHandler<UpdateClaimCommandRequest, UpdateClaimCommandResponse>
    {
        public Task<UpdateClaimCommandResponse> Handle(UpdateClaimCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada talep güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

}
