using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Brand.DeleteBrand
{
    public class DeleteBrandCommandRequest : IRequest<DeleteBrandCommandResponse>
    {
        // Buraya marka silme için gerekli alanlar eklenebilir, örneğin brandId gibi
    }

    public class DeleteBrandCommandResponse
    {
        // İsteğe bağlı olarak silme sonucuyla ilgili bilgiler eklenebilir
    }

    public class DeleteBrandCommandRequestHandler : IRequestHandler<DeleteBrandCommandRequest, DeleteBrandCommandResponse>
    {
        public Task<DeleteBrandCommandResponse> Handle(DeleteBrandCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada marka silme işleminin kodunu yazmanız gerekecek
        }
    }

    public class DeleteBrandCommandRequestValidator : AbstractValidator<DeleteBrandCommandRequest>
    {
        public DeleteBrandCommandRequestValidator()
        {
            // Buraya marka silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
