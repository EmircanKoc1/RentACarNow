using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Queries.Brand.GetAll
{
    public class GetAllBrandQueryRequest : IRequest<IEnumerable<GetAllBrandQueryResponse>>
    {

    }

    public class GetAllBrandQueryResponse
    {

    }

    public class GetAllBrandQueryRequestHandler : IRequestHandler<GetAllBrandQueryRequest, IEnumerable<GetAllBrandQueryResponse>>
    {
        public Task<IEnumerable<GetAllBrandQueryResponse>> Handle(GetAllBrandQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetAllBrandQueryRequestValidator : AbstractValidator<GetAllBrandQueryRequest>
    {
        public GetAllBrandQueryRequestValidator()
        {

        }
    }

}
