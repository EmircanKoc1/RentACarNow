using FluentValidation;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.UpdateEmployee
{

    public class UpdateEmployeeCommandRequestValidator : AbstractValidator<UpdateEmployeeCommandRequest>
    {
        public UpdateEmployeeCommandRequestValidator()
        {
            // Buraya çalışan güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
