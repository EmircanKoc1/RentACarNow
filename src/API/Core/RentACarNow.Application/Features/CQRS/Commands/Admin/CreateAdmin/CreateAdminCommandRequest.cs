using FluentValidation;
using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequest : IRequest<CreateAdminCommandResponse>
    {
    }
    public class CreateAdminCommandResponse
    {
    }
    public class CreateAdminCommandRequestHandler :  IRequestHandler<CreateAdminCommandRequest, CreateAdminCommandResponse>
    {
        public Task<CreateAdminCommandResponse> Handle(CreateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class CreateAdminCommandRequestValidator : AbstractValidator<CreateAdminCommandRequest>
    {
        public CreateAdminCommandRequestValidator()
        {
            
        }
    }

}
