using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Queries.Rental.GetById
{
    public class GetByIdRentalQueryRequest : IRequest<IEnumerable<GetByIdRentalQueryResponse>>
    {

    }

    public class GetByIdRentalQueryResponse
    {

    }


    public class GetByIdRentalQueryRequestHandler : IRequestHandler<GetByIdRentalQueryRequest, IEnumerable<GetByIdRentalQueryResponse>>
    {
        public Task<IEnumerable<GetByIdRentalQueryResponse>> Handle(GetByIdRentalQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdRentalQueryRequestValidator : AbstractValidator<GetByIdRentalQueryRequest>
    {
        public GetByIdRentalQueryRequestValidator()
        {


        }
    }

}
