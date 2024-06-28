using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Admin.UpdateAdmin
{
    public class UpdateAdminCommandRequest : IRequest<UpdateAdminCommandResponse>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateAdminCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateAdminCommandRequestHandler : IRequestHandler<UpdateAdminCommandRequest, UpdateAdminCommandResponse>
    {
        public Task<UpdateAdminCommandResponse> Handle(UpdateAdminCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateAdminCommandRequestValidator : AbstractValidator<UpdateAdminCommandRequest>
    {
        public UpdateAdminCommandRequestValidator()
        {
            // Buraya güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
