using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequestValidator : AbstractValidator<UpdateClaimCommandRequest>
    {
        public UpdateClaimCommandRequestValidator()
        {
            // Buraya talep güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
