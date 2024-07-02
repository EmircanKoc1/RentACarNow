using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Admin.UpdateAdmin
{
    public class UpdateAdminCommandRequestValidator : AbstractValidator<UpdateAdminCommandRequest>
    {
        public UpdateAdminCommandRequestValidator()
        {
            // Buraya güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
