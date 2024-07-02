using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Feature.DeleteFeature
{

    public class DeleteFeatureCommandRequestValidator : AbstractValidator<DeleteFeatureCommandRequest>
    {
        public DeleteFeatureCommandRequestValidator()
        {
            // Buraya özellik silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
