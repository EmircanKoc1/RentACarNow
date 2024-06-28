using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.CreateEmployee
{

    public class CreateEmployeeCommandRequestValidator : AbstractValidator<CreateEmployeeCommandRequest>
    {
        public CreateEmployeeCommandRequestValidator()
        {
            // Buraya çalışan oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
