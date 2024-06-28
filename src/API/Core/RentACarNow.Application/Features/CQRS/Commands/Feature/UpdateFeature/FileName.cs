using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.UpdateFeature
{
    public class UpdateFeatureCommandRequest : IRequest<UpdateFeatureCommandResponse>
    {
        // Buraya özellik güncelleme için gerekli alanlar eklenebilir, örneğin featureId, yeni bilgiler gibi
    }

    public class UpdateFeatureCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateFeatureCommandRequestHandler : IRequestHandler<UpdateFeatureCommandRequest, UpdateFeatureCommandResponse>
    {
        public Task<UpdateFeatureCommandResponse> Handle(UpdateFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada özellik güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateFeatureCommandRequestValidator : AbstractValidator<UpdateFeatureCommandRequest>
    {
        public UpdateFeatureCommandRequestValidator()
        {
            // Buraya özellik güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
