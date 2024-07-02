using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequestValidator : AbstractValidator<DeleteClaimCommandRequest>
    {
        public DeleteClaimCommandRequestValidator()
        {
            // Buraya talep silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
