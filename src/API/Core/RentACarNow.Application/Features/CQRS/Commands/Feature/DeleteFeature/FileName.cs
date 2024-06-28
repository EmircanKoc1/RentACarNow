using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Feature.DeleteFeature
{
    public class DeleteFeatureCommandRequest : IRequest<DeleteFeatureCommandResponse>
    {
        // Buraya özellik silme için gerekli alanlar eklenebilir, örneğin featureId gibi
    }

    public class DeleteFeatureCommandResponse
    {
        // İsteğe bağlı olarak silme sonucuyla ilgili bilgiler eklenebilir
    }

    public class DeleteFeatureCommandRequestHandler : IRequestHandler<DeleteFeatureCommandRequest, DeleteFeatureCommandResponse>
    {
        public Task<DeleteFeatureCommandResponse> Handle(DeleteFeatureCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada özellik silme işleminin kodunu yazmanız gerekecek
        }
    }

    public class DeleteFeatureCommandRequestValidator : AbstractValidator<DeleteFeatureCommandRequest>
    {
        public DeleteFeatureCommandRequestValidator()
        {
            // Buraya özellik silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
