using FluentValidation;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.UpdateEmployee
{

    public class UpdateEmployeeCommandRequestValidator : AbstractValidator<UpdateEmployeeCommandRequest>
    {
        public UpdateEmployeeCommandRequestValidator()
        {
            // Buraya çalışan güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
