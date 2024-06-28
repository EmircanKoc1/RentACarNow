using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequestHandler : IRequestHandler<CreateAdminCommandRequest, CreateAdminCommandResponse>
    {
        public Task<CreateAdminCommandResponse> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
