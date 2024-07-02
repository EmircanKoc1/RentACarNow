using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequestHandler : IRequestHandler<DeleteBrandCommandRequest, DeleteBrandCommandResponse>
    {
        public Task<DeleteBrandCommandResponse> Handle(DeleteBrandCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada marka silme işleminin kodunu yazmanız gerekecek
        }
    }

}
