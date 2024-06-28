using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.DeleteClaim
{
    public class DeleteClaimCommandRequest : IRequest<DeleteClaimCommandResponse>
    {
        public Guid Id { get; set; }

    }

    public class DeleteClaimCommandResponse
    {
        // İsteğe bağlı olarak silme sonucuyla ilgili bilgiler eklenebilir
    }

    public class DeleteClaimCommandRequestHandler : IRequestHandler<DeleteClaimCommandRequest, DeleteClaimCommandResponse>
    {
        public Task<DeleteClaimCommandResponse> Handle(DeleteClaimCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada talep silme işleminin kodunu yazmanız gerekecek
        }
    }

    public class DeleteClaimCommandRequestValidator : AbstractValidator<DeleteClaimCommandRequest>
    {
        public DeleteClaimCommandRequestValidator()
        {
            // Buraya talep silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
