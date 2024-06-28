using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.DeleteAdmin
{
    public class DeleteAdminCommandRequestValidator : AbstractValidator<DeleteAdminCommandRequest>
    {
        public DeleteAdminCommandRequestValidator()
        {

        }
    }

}
