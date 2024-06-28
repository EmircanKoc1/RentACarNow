using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.UpdateFeature
{
    public class UpdateFeatureCommandRequestValidator : AbstractValidator<UpdateFeatureCommandRequest>
    {
        public UpdateFeatureCommandRequestValidator()
        {
            // Buraya özellik güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
