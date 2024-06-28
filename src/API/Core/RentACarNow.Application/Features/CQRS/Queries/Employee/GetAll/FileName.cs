using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Queries.Employee.GetAll
{
    public class GetAllEmployeeQueryRequest : IRequest<IEnumerable<GetAllEmployeeQueryResponse>>
    {

    }

    public class GetAllEmployeeQueryResponse
    {

    }

    public class GetAllEmployeeQueryRequestHandler : IRequestHandler<GetAllEmployeeQueryRequest, IEnumerable<GetAllEmployeeQueryResponse>>
    {
        public Task<IEnumerable<GetAllEmployeeQueryResponse>> Handle(GetAllEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetAllEmployeeQueryRequestValidator : AbstractValidator<GetAllEmployeeQueryRequest>
    {
        public GetAllEmployeeQueryRequestValidator()
        {

        }
    }

}
