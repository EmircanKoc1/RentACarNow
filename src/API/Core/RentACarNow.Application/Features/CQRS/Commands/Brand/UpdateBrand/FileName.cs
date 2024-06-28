using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.UpdateBrand
{
    public class UpdateBrandCommandRequest : IRequest<UpdateBrandCommandResponse>
    {
        public Guid Id { get; set; }

    }

    public class UpdateBrandCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateBrandCommandRequestHandler : IRequestHandler<UpdateBrandCommandRequest, UpdateBrandCommandResponse>
    {
        public Task<UpdateBrandCommandResponse> Handle(UpdateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada marka güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateBrandCommandRequestValidator : AbstractValidator<UpdateBrandCommandRequest>
    {
        public UpdateBrandCommandRequestValidator()
        {
            // Buraya marka güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
