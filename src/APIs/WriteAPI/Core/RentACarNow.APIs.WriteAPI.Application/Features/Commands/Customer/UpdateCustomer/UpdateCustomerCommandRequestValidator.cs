using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandRequestValidator : AbstractValidator<UpdateCustomerCommandRequest>
    {
        public UpdateCustomerCommandRequestValidator()
        {
            // Buraya müşteri güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
