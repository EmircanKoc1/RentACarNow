using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequestValidator : AbstractValidator<CreateBrandCommandRequest>
    {
        public CreateBrandCommandRequestValidator()
        {
            // Buraya marka oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }
    
}
