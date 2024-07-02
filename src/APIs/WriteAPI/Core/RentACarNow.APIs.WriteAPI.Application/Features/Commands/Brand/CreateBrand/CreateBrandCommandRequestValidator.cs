using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequestValidator : AbstractValidator<CreateBrandCommandRequest>
    {
        public CreateBrandCommandRequestValidator()
        {
            // Buraya marka oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
