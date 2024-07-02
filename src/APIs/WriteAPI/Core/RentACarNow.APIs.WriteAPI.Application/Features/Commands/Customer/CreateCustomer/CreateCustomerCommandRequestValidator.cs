using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandRequestValidator : AbstractValidator<CreateCustomerCommandRequest>
    {
        public CreateCustomerCommandRequestValidator()
        {
            // Buraya müşteri oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
