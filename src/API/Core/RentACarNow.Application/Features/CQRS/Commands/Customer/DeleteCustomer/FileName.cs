using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.DeleteCustomer
{
    public class DeleteCustomerCommandRequest : IRequest<DeleteCustomerCommandResponse>
    {
        // Buraya müşteri silme için gerekli alanlar eklenebilir, örneğin customerId gibi
    }

    public class DeleteCustomerCommandResponse
    {
        // İsteğe bağlı olarak silme sonucuyla ilgili bilgiler eklenebilir
    }

    public class DeleteCustomerCommandRequestHandler : IRequestHandler<DeleteCustomerCommandRequest, DeleteCustomerCommandResponse>
    {
        public Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada müşteri silme işleminin kodunu yazmanız gerekecek
        }
    }

    public class DeleteCustomerCommandRequestValidator : AbstractValidator<DeleteCustomerCommandRequest>
    {
        public DeleteCustomerCommandRequestValidator()
        {
            // Buraya müşteri silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
