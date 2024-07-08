using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetById
{

    public class GetByIdRentalQueryRequestValidator : AbstractValidator<GetByIdRentalQueryRequest>
    {
        public GetByIdRentalQueryRequestValidator()
        {


        }
    }

}
