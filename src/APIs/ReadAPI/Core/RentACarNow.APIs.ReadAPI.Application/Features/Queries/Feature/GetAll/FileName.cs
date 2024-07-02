using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetAll
{
    public class GetAllFeatureQueryRequest : IRequest<IEnumerable<GetAllFeatureQueryResponse>>
    {

    }

    public class GetAllFeatureQueryResponse
    {

    }

    public class GetAllFeatureQueryRequestHandler : IRequestHandler<GetAllFeatureQueryRequest, IEnumerable<GetAllFeatureQueryResponse>>
    {
        public Task<IEnumerable<GetAllFeatureQueryResponse>> Handle(GetAllFeatureQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada Feature ile ilgili sorgu işleminin kodunu yazmanız gerekecek
        }
    }

    public class GetAllFeatureQueryRequestValidator : AbstractValidator<GetAllFeatureQueryRequest>
    {
        public GetAllFeatureQueryRequestValidator()
        {
            // Burada Feature ile ilgili sorgu komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
