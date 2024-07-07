using MediatR;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToAdmin
{
    public class AddClaimToAdminCommandRequestHandler : IRequestHandler<AddClaimToAdminCommandRequest, AddClaimToAdminCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _repository;

        public AddClaimToAdminCommandRequestHandler(IEfCoreClaimWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddClaimToAdminCommandResponse> Handle(AddClaimToAdminCommandRequest request, CancellationToken cancellationToken)
        {

            await _repository.AddClaimToAdminAsync(request.ClaimId, request.AdminId);

            return new AddClaimToAdminCommandResponse();
        }
    }
}
