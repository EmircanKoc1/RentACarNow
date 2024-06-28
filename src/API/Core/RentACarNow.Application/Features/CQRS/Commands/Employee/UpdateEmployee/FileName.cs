using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommandRequest : IRequest<UpdateEmployeeCommandResponse>
    {
        // Buraya çalışan güncelleme için gerekli alanlar eklenebilir, örneğin employeeId, yeni bilgiler gibi
    }

    public class UpdateEmployeeCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateEmployeeCommandRequestHandler : IRequestHandler<UpdateEmployeeCommandRequest, UpdateEmployeeCommandResponse>
    {
        public Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada çalışan güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateEmployeeCommandRequestValidator : AbstractValidator<UpdateEmployeeCommandRequest>
    {
        public UpdateEmployeeCommandRequestValidator()
        {
            // Buraya çalışan güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
