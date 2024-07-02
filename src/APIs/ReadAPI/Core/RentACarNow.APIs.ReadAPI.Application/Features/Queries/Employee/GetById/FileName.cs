using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetById
{
    public class GetByIdEmployeeQueryRequest : IRequest<IEnumerable<GetByIdEmployeeQueryResponse>>
    {

    }

    public class GetByIdEmployeeQueryResponse
    {

    }


    public class GetByIdEmployeeQueryRequestHandler : IRequestHandler<GetByIdEmployeeQueryRequest, IEnumerable<GetByIdEmployeeQueryResponse>>
    {
        public Task<IEnumerable<GetByIdEmployeeQueryResponse>> Handle(GetByIdEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdEmployeeQueryRequestValidator : AbstractValidator<GetByIdEmployeeQueryRequest>
    {
        public GetByIdEmployeeQueryRequestValidator()
        {


        }
    }

}
