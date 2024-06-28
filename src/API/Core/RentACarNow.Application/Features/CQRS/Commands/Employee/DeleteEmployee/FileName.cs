using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommandRequest : IRequest<DeleteEmployeeCommandResponse>
    {
        // Buraya çalışan silme için gerekli alanlar eklenebilir, örneğin employeeId gibi
    }

    public class DeleteEmployeeCommandResponse
    {
        // İsteğe bağlı olarak silme sonucuyla ilgili bilgiler eklenebilir
    }

    public class DeleteEmployeeCommandRequestHandler : IRequestHandler<DeleteEmployeeCommandRequest, DeleteEmployeeCommandResponse>
    {
        public Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada çalışan silme işleminin kodunu yazmanız gerekecek
        }
    }

    public class DeleteEmployeeCommandRequestValidator : AbstractValidator<DeleteEmployeeCommandRequest>
    {
        public DeleteEmployeeCommandRequestValidator()
        {
            // Buraya çalışan silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
