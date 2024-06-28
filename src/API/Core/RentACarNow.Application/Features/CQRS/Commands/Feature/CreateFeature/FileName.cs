using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.CreateFeature
{
    public class CreateFeatureCommandRequest : IRequest<CreateFeatureCommandResponse>
    {
        // Buraya özellik oluşturma için gerekli alanlar eklenebilir, örneğin ad, açıklama gibi
    }

    public class CreateFeatureCommandResponse
    {
        // İsteğe bağlı olarak oluşturma sonucuyla ilgili bilgiler eklenebilir
    }

    public class CreateFeatureCommandRequestHandler : IRequestHandler<CreateFeatureCommandRequest, CreateFeatureCommandResponse>
    {
        public Task<CreateFeatureCommandResponse> Handle(CreateFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada özellik oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

    public class CreateFeatureCommandRequestValidator : AbstractValidator<CreateFeatureCommandRequest>
    {
        public CreateFeatureCommandRequestValidator()
        {
            // Buraya özellik oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
