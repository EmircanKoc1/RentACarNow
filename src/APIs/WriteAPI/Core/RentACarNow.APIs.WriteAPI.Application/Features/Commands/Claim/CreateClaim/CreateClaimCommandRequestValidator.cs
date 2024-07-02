using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequestValidator : AbstractValidator<CreateClaimCommandRequest>
    {
        public CreateClaimCommandRequestValidator()
        {
            // Buraya talep oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
