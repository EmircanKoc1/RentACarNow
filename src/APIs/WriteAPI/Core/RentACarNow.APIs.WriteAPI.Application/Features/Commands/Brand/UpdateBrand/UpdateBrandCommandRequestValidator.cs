using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.UpdateBrand
{
    public class UpdateBrandCommandRequestValidator : AbstractValidator<UpdateBrandCommandRequest>
    {
        public UpdateBrandCommandRequestValidator()
        {
            // Buraya marka güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
