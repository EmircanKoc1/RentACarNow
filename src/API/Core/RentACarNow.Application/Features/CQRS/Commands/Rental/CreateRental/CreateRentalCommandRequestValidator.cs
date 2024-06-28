using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.Application.Features.CQRS.Commands.Rental.CreateRental
{

    public class CreateRentalCommandRequestValidator : AbstractValidator<CreateRentalCommandRequest>
    {
        public CreateRentalCommandRequestValidator()
        {
            // Buraya kiralama oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
