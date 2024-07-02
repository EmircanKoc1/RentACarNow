using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Rental.CreateRental
{

    public class CreateRentalCommandRequestValidator : AbstractValidator<CreateRentalCommandRequest>
    {
        public CreateRentalCommandRequestValidator()
        {
            // Buraya kiralama oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
