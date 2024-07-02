using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Queries.Admin.GetById
{
    public class GetByIdAdminQueryRequest : IRequest<IEnumerable<GetByIdAdminQueryResponse>>
    {

    }

    public class GetByIdAdminQueryResponse
    {

    }


    public class GetByIdAdminQueryRequestHandler : IRequestHandler<GetByIdAdminQueryRequest, IEnumerable<GetByIdAdminQueryResponse>>
    {
        public Task<IEnumerable<GetByIdAdminQueryResponse>> Handle(GetByIdAdminQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdAdminQueryRequestValidator : AbstractValidator<GetByIdAdminQueryRequest>
    {
        public GetByIdAdminQueryRequestValidator()
        {


        }


    }
}
