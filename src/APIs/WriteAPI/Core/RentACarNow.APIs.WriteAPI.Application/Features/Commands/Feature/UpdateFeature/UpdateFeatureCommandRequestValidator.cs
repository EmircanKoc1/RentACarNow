using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.UpdateFeature
{
    public class UpdateFeatureCommandRequestValidator : AbstractValidator<UpdateFeatureCommandRequest>
    {
        public UpdateFeatureCommandRequestValidator()
        {
            // Buraya özellik güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
