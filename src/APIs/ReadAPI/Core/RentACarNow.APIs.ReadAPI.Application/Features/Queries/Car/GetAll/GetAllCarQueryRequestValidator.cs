using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetAll
{

    public class GetAllCarQueryRequestValidator : AbstractValidator<GetAllCarQueryRequest>
    {
        public GetAllCarQueryRequestValidator()
        {

        }
    }

}
