using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandRequest : IRequest<UpdateCustomerCommandResponse>
    {
        // Buraya müşteri güncelleme için gerekli alanlar eklenebilir, örneğin customerId, yeni bilgiler gibi
    }

    public class UpdateCustomerCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateCustomerCommandRequestHandler : IRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        public Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada müşteri güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateCustomerCommandRequestValidator : AbstractValidator<UpdateCustomerCommandRequest>
    {
        public UpdateCustomerCommandRequestValidator()
        {
            // Buraya müşteri güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
