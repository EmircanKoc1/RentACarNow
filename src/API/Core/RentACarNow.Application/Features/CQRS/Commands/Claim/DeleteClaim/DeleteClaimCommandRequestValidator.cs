using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequestValidator : AbstractValidator<DeleteClaimCommandRequest>
    {
        public DeleteClaimCommandRequestValidator()
        {
            // Buraya talep silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
