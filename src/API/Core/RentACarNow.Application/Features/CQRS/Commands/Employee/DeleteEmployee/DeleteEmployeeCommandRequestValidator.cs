﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.DeleteEmployee
{

    public class DeleteEmployeeCommandRequestValidator : AbstractValidator<DeleteEmployeeCommandRequest>
    {
        public DeleteEmployeeCommandRequestValidator()
        {
            // Buraya çalışan silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}