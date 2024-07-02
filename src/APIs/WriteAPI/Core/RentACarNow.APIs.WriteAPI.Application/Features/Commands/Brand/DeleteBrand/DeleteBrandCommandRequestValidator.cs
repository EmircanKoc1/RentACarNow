using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequestValidator : AbstractValidator<DeleteBrandCommandRequest>
    {
        public DeleteBrandCommandRequestValidator()
        {
            // Buraya marka silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
