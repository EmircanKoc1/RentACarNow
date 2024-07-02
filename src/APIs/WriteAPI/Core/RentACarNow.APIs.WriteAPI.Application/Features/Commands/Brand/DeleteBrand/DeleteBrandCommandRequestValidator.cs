using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequestValidator : AbstractValidator<DeleteBrandCommandRequest>
    {
        public DeleteBrandCommandRequestValidator()
        {
            // Buraya marka silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
