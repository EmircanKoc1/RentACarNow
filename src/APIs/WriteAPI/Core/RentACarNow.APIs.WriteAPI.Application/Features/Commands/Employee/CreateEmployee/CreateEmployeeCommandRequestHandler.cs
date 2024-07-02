using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.CreateEmployee
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
