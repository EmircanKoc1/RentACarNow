using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.CreateFeature
{

    public class CreateFeatureCommandRequestValidator : AbstractValidator<CreateFeatureCommandRequest>
    {
        public CreateFeatureCommandRequestValidator()
        {
            // Buraya özellik oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
