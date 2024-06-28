using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Queries.Customer.GetById
{
    public class GetByIdCustomerQueryRequest : IRequest<IEnumerable<GetByIdCustomerQueryResponse>>
    {

    }

    public class GetByIdCustomerQueryResponse
    {

    }


    public class GetByIdCustomerQueryRequestHandler : IRequestHandler<GetByIdCustomerQueryRequest, IEnumerable<GetByIdCustomerQueryResponse>>
    {
        public Task<IEnumerable<GetByIdCustomerQueryResponse>> Handle(GetByIdCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdCustomerQueryRequestValidator : AbstractValidator<GetByIdCustomerQueryRequest>
    {
        public GetByIdCustomerQueryRequestValidator()
        {


        }
    }

}
