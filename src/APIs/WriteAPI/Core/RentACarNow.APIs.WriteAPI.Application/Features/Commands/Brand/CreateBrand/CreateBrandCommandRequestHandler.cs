using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequestHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
    {
        public Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada marka oluşturma işleminin kodunu yazmanız gerekecek
        }
    }
    
}
