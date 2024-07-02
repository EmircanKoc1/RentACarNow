using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequestValidator : AbstractValidator<CreateClaimCommandRequest>
    {
        public CreateClaimCommandRequestValidator()
        {
            // Buraya talep oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
