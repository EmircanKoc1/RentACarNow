using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById
{
    public class GetByIdRentalQueryRequest : IRequest<ResponseWrapper<GetByIdRentalQueryResponse>>
    {
        public Guid RentalId { get; set; }

    }

}
