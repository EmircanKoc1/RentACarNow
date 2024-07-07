using MediatR;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.AddClaimToEmployee
{
    public class AddClaimToEmployeeCommandRequestHandler : IRequestHandler<AddClaimToEmployeeCommandRequest, AddClaimToEmployeeCommandResponse>
    {
        private readonly IEfCoreClaimWriteRepository _repository;

        public AddClaimToEmployeeCommandRequestHandler(IEfCoreClaimWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddClaimToEmployeeCommandResponse> Handle(AddClaimToEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.AddClaimToEmployeeAsync(request.ClaimId, request.EmployeeId);

            return new AddClaimToEmployeeCommandResponse();
        }
    }
}
