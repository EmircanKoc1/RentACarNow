using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll
{
    public class GetAllClaimQueryRequest : IRequest<IEnumerable<GetAllClaimQueryResponse>>
    {

    }

    public class GetAllClaimQueryResponse
    {

    }

    public class GetAllClaimQueryRequestHandler : IRequestHandler<GetAllClaimQueryRequest, IEnumerable<GetAllClaimQueryResponse>>
    {
        public Task<IEnumerable<GetAllClaimQueryResponse>> Handle(GetAllClaimQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetAllClaimQueryRequestValidator : AbstractValidator<GetAllClaimQueryRequest>
    {
        public GetAllClaimQueryRequestValidator()
        {

        }
    }

}
