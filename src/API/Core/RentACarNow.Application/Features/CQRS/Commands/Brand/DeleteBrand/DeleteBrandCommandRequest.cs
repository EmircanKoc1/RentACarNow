using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequest : IRequest<DeleteBrandCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
