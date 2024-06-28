using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.CreateClaim
{
    public class CreateClaimCommandRequest : IRequest<CreateClaimCommandResponse>
    {
        // Buraya talep oluşturma için gerekli alanlar eklenebilir, örneğin kullanıcı adı, talep detayları gibi
    }

    public class CreateClaimCommandResponse
    {
        // İsteğe bağlı olarak oluşturma sonucuyla ilgili bilgiler eklenebilir
    }

    public class CreateClaimCommandRequestHandler : IRequestHandler<CreateClaimCommandRequest, CreateClaimCommandResponse>
    {
        public Task<CreateClaimCommandResponse> Handle(CreateClaimCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada talep oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

    public class CreateClaimCommandRequestValidator : AbstractValidator<CreateClaimCommandRequest>
    {
        public CreateClaimCommandRequestValidator()
        {
            // Buraya talep oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
