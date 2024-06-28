using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.DeleteAdmin
{
    public class DeleteAdminCommandRequestHandler : IRequestHandler<DeleteAdminCommandRequest, DeleteAdminCommandResponse>
    {
        public Task<DeleteAdminCommandResponse> Handle(DeleteAdminCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
