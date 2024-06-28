using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.CreateEmployee
{
    public class CreateEmployeeCommandRequest : IRequest<CreateEmployeeCommandResponse>
    {
        // Buraya çalışan oluşturma için gerekli alanlar eklenebilir, örneğin ad, soyad, email gibi
    }

    public class CreateEmployeeCommandResponse
    {
        // İsteğe bağlı olarak oluşturma sonucuyla ilgili bilgiler eklenebilir
    }

    public class CreateEmployeeCommandRequestHandler : IRequestHandler<CreateEmployeeCommandRequest, CreateEmployeeCommandResponse>
    {
        public Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada çalışan oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

    public class CreateEmployeeCommandRequestValidator : AbstractValidator<CreateEmployeeCommandRequest>
    {
        public CreateEmployeeCommandRequestValidator()
        {
            // Buraya çalışan oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
