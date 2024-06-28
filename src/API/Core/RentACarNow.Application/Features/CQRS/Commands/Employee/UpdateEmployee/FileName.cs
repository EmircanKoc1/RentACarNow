using FluentValidation;
using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommandRequest : IRequest<UpdateEmployeeCommandResponse>
    {
        public Guid Id { get; set; }

    }

    public class UpdateEmployeeCommandResponse
    {
        // İsteğe bağlı olarak güncelleme sonucuyla ilgili bilgiler eklenebilir
    }

    public class UpdateEmployeeCommandRequestHandler : IRequestHandler<UpdateEmployeeCommandRequest, UpdateEmployeeCommandResponse>
    {
        public Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada çalışan güncelleme işleminin kodunu yazmanız gerekecek
        }
    }

    public class UpdateEmployeeCommandRequestValidator : AbstractValidator<UpdateEmployeeCommandRequest>
    {
        public UpdateEmployeeCommandRequestValidator()
        {
            // Buraya çalışan güncelleme komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
