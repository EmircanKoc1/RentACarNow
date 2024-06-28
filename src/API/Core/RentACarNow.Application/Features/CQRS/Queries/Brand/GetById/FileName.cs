using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Queries.Brand.GetById
{
    public class GetByIdBrandQueryRequest : IRequest<IEnumerable<GetByIdBrandQueryResponse>>
    {

    }

    public class GetByIdBrandQueryResponse
    {

    }


    public class GetByIdBrandQueryRequestHandler : IRequestHandler<GetByIdBrandQueryRequest, IEnumerable<GetByIdBrandQueryResponse>>
    {
        public Task<IEnumerable<GetByIdBrandQueryResponse>> Handle(GetByIdBrandQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdBrandQueryRequestValidator : AbstractValidator<GetByIdBrandQueryRequest>
    {
        public GetByIdBrandQueryRequestValidator()
        {


        }
    }

}
