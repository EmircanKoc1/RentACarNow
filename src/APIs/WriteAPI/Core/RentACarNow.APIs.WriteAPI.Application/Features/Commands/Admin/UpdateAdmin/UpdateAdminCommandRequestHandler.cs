using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.UpdateAdmin
{
    public class UpdateAdminCommandRequestHandler : IRequestHandler<UpdateAdminCommandRequest, UpdateAdminCommandResponse>
    {
        public Task<UpdateAdminCommandResponse> Handle(UpdateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

}
