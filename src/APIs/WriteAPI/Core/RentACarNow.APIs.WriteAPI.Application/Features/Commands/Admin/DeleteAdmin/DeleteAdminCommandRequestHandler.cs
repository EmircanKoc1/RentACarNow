using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.DeleteAdmin
{
    public class DeleteAdminCommandRequestHandler : IRequestHandler<DeleteAdminCommandRequest, DeleteAdminCommandResponse>
    {
        public Task<DeleteAdminCommandResponse> Handle(DeleteAdminCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
