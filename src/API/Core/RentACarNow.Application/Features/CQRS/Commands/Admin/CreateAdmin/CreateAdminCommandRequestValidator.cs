using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.CreateAdmin
{
    public class CreateAdminCommandRequestValidator : AbstractValidator<CreateAdminCommandRequest>
    {
        public CreateAdminCommandRequestValidator()
        {

        }
    }

}
