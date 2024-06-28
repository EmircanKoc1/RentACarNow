using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.CreateEmployee
{
    public class CreateEmployeeCommandRequestHandler : IRequestHandler<CreateEmployeeCommandRequest, CreateEmployeeCommandResponse>
    {
        public Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada çalışan oluşturma işleminin kodunu yazmanız gerekecek
        }
    }

}
