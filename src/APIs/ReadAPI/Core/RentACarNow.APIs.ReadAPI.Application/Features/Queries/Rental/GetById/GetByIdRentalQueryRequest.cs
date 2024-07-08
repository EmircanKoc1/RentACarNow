using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById
{
    public class GetByIdRentalQueryRequest : IRequest<IEnumerable<GetByIdRentalQueryResponse>>
    {
        public Guid Id { get; set; }

    }

}
