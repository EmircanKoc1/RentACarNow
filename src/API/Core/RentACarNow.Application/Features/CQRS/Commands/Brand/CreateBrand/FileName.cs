using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.CreateBrand
{
    public class CreateBrandCommandRequest : IRequest<CreateBrandCommandResponse>
    {
        // Buraya marka oluşturma için gerekli alanlar eklenebilir
    }

    public class CreateBrandCommandResponse
    {
        // İsteğe bağlı olarak oluşturma sonucuyla ilgili bilgiler eklenebilir
    }

    public class CreateBrandCommandRequestHandler : IRequestHandler<CreateBrandCommandRequest, CreateBrandCommandResponse>
    {
        public Task<CreateBrandCommandResponse> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada marka oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

    public class CreateBrandCommandRequestValidator : AbstractValidator<CreateBrandCommandRequest>
    {
        public CreateBrandCommandRequestValidator()
        {
            // Buraya marka oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }
    
}
