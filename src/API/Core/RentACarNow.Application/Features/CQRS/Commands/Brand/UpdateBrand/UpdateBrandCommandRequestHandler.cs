using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.UpdateBrand
{
    public class UpdateBrandCommandRequestHandler : IRequestHandler<UpdateBrandCommandRequest, UpdateBrandCommandResponse>
    {
        public Task<UpdateBrandCommandResponse> Handle(UpdateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada marka güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

}
