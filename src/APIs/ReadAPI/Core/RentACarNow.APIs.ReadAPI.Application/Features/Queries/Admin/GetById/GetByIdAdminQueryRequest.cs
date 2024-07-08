using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetById
{
    public class GetByIdAdminQueryRequest : IRequest<GetByIdAdminQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
