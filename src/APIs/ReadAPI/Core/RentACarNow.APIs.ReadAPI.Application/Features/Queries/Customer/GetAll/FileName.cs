using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetAll
{
    public class GetAllCustomerQueryRequest : IRequest<IEnumerable<GetAllCustomerQueryResponse>>
    {

    }

    public class GetAllCustomerQueryResponse
    {

    }

    public class GetAllCustomerQueryRequestHandler : IRequestHandler<GetAllCustomerQueryRequest, IEnumerable<GetAllCustomerQueryResponse>>
    {
        public Task<IEnumerable<GetAllCustomerQueryResponse>> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetAllCustomerQueryRequestValidator : AbstractValidator<GetAllCustomerQueryRequest>
    {
        public GetAllCustomerQueryRequestValidator()
        {

        }
    }

}
