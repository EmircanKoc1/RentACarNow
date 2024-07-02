using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Feature.GetById
{
    public class GetByIdFeatureQueryRequest : IRequest<IEnumerable<GetByIdFeatureQueryResponse>>
    {

    }

    public class GetByIdFeatureQueryResponse
    {

    }


    public class GetByIdFeatureQueryRequestHandler : IRequestHandler<GetByIdFeatureQueryRequest, IEnumerable<GetByIdFeatureQueryResponse>>
    {
        public Task<IEnumerable<GetByIdFeatureQueryResponse>> Handle(GetByIdFeatureQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdFeatureQueryRequestValidator : AbstractValidator<GetByIdFeatureQueryRequest>
    {
        public GetByIdFeatureQueryRequestValidator()
        {


        }
    }

}
