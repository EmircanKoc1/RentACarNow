using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandRequest : IRequest<CreateCustomerCommandResponse>
    {
        // Buraya müşteri oluşturma için gerekli alanlar eklenebilir, örneğin ad, soyad, email gibi
    }

    public class CreateCustomerCommandResponse
    {
        // İsteğe bağlı olarak oluşturma sonucuyla ilgili bilgiler eklenebilir
    }

    public class CreateCustomerCommandRequestHandler : IRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        public Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada müşteri oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

    public class CreateCustomerCommandRequestValidator : AbstractValidator<CreateCustomerCommandRequest>
    {
        public CreateCustomerCommandRequestValidator()
        {
            // Buraya müşteri oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
