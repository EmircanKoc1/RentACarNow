using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById
{
    public class GetByIdCarQueryRequest : IRequest<IEnumerable<GetByIdCarQueryResponse>>
    {

    }

    public class GetByIdCarQueryResponse
    {

    }


    public class GetByIdCarQueryRequestHandler : IRequestHandler<GetByIdCarQueryRequest, IEnumerable<GetByIdCarQueryResponse>>
    {
        public Task<IEnumerable<GetByIdCarQueryResponse>> Handle(GetByIdCarQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdCarQueryRequestValidator : AbstractValidator<GetByIdCarQueryRequest>
    {
        public GetByIdCarQueryRequestValidator()
        {


        }
    }

}
