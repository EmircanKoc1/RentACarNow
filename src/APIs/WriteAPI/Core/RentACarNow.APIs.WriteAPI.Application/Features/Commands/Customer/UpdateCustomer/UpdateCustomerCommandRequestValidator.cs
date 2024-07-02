using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandRequestValidator : AbstractValidator<UpdateCustomerCommandRequest>
    {
        public UpdateCustomerCommandRequestValidator()
        {
            // Buraya müşteri güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
