using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequestHandler : IRequestHandler<CreateClaimCommandRequest, CreateClaimCommandResponse>
    {
        public Task<CreateClaimCommandResponse> Handle(CreateClaimCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada talep oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

}
