using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.UpdateBrand
{
    public class UpdateBrandCommandRequestValidator : AbstractValidator<UpdateBrandCommandRequest>
    {
        public UpdateBrandCommandRequestValidator()
        {
            // Buraya marka güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
