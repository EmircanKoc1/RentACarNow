using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequestValidator : AbstractValidator<UpdateClaimCommandRequest>
    {
        public UpdateClaimCommandRequestValidator()
        {
            // Buraya talep güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
