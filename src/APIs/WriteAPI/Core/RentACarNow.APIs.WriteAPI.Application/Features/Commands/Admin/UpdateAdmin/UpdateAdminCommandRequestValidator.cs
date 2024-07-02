using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.UpdateAdmin
{
    public class UpdateAdminCommandRequestValidator : AbstractValidator<UpdateAdminCommandRequest>
    {
        public UpdateAdminCommandRequestValidator()
        {
            // Buraya güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
