using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Customer.GetById
{

    public class GetByIdCustomerQueryRequestValidator : AbstractValidator<GetByIdCustomerQueryRequest>
    {
        public GetByIdCustomerQueryRequestValidator()
        {


        }
    }

}
