using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.CreateCustomer
{
    public class CreateCustomerCommandRequestValidator : AbstractValidator<CreateCustomerCommandRequest>
    {
        public CreateCustomerCommandRequestValidator()
        {
            // Buraya müşteri oluşturma komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
