using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.DeleteCustomer
{

    public class DeleteCustomerCommandRequestValidator : AbstractValidator<DeleteCustomerCommandRequest>
    {
        public DeleteCustomerCommandRequestValidator()
        {
            // Buraya müşteri silme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
