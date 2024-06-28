using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Claim.UpdateClaim
{
    public class UpdateClaimCommandRequest : IRequest<UpdateClaimCommandResponse>
    {
        public Guid Id { get; set; }

    }

    public class UpdateClaimCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateClaimCommandRequestHandler : IRequestHandler<UpdateClaimCommandRequest, UpdateClaimCommandResponse>
    {
        public Task<UpdateClaimCommandResponse> Handle(UpdateClaimCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada talep güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateClaimCommandRequestValidator : AbstractValidator<UpdateClaimCommandRequest>
    {
        public UpdateClaimCommandRequestValidator()
        {
            // Buraya talep güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
