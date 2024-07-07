using MediatR;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToCustomer
{
    public class AddClaimToCustomerCommandRequestHandler : IRequestHandler<AddClaimToCustomerCommandRequest, AddClaimToCustomerCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _repository;

        public AddClaimToCustomerCommandRequestHandler(IEfCoreClaimWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddClaimToCustomerCommandResponse> Handle(AddClaimToCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.AddClaimToCustomerAsync(request.ClaimId, request.CustomerId);

            return new AddClaimToCustomerCommandResponse();
        }
    }
}
