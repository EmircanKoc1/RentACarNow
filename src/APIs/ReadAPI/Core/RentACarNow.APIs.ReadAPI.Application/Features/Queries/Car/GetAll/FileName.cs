using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll
{
    public class GetAllCarQueryRequest : IRequest<IEnumerable<GetAllCarQueryResponse>>
    {

    }

    public class GetAllCarQueryResponse
    {

    }

    public class GetAllCarQueryRequestHandler : IRequestHandler<GetAllCarQueryRequest, IEnumerable<GetAllCarQueryResponse>>
    {
        public Task<IEnumerable<GetAllCarQueryResponse>> Handle(GetAllCarQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetAllCarQueryRequestValidator : AbstractValidator<GetAllCarQueryRequest>
    {
        public GetAllCarQueryRequestValidator()
        {

        }
    }

}
