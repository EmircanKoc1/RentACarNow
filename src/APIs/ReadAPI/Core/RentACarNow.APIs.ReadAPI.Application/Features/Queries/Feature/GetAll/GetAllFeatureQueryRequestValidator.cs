using FluentValidation;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll
{
    public class GetAllFeatureQueryRequestValidator : AbstractValidator<GetAllFeatureQueryRequest>
    {
        public GetAllFeatureQueryRequestValidator()
        {
            // Burada Feature ile ilgili sorgu komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
