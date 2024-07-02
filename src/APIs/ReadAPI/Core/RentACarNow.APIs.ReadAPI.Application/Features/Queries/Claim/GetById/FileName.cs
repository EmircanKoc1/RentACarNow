using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetById
{
    public class GetByIdClaimQueryRequest : IRequest<IEnumerable<GetByIdClaimQueryResponse>>
    {

    }

    public class GetByIdClaimQueryResponse
    {

    }


    public class GetByIdClaimQueryRequestHandler : IRequestHandler<GetByIdClaimQueryRequest, IEnumerable<GetByIdClaimQueryResponse>>
    {
        public Task<IEnumerable<GetByIdClaimQueryResponse>> Handle(GetByIdClaimQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdClaimQueryRequestValidator : AbstractValidator<GetByIdClaimQueryRequest>
    {
        public GetByIdClaimQueryRequestValidator()
        {


        }
    }

}
