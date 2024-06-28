using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.DeleteAdmin
{
    public class DeleteAdminCommandRequest : IRequest<DeleteAdminCommandResponse>
    {
    }

    public class DeleteAdminCommandResponse
    {
    }

    public class DeleteAdminCommandRequestHandler : IRequestHandler<DeleteAdminCommandRequest, DeleteAdminCommandResponse>
    {
        public Task<DeleteAdminCommandResponse> Handle(DeleteAdminCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class DeleteAdminCommandRequestValidator : AbstractValidator<DeleteAdminCommandRequest>
    {
        public DeleteAdminCommandRequestValidator()
        {

        }
    }

}
