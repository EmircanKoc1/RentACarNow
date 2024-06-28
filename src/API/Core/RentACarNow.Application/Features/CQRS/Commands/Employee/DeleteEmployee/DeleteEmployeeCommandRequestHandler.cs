using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommandRequestHandler : IRequestHandler<DeleteEmployeeCommandRequest, DeleteEmployeeCommandResponse>
    {
        public Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada çalışan silme işleminin kodunu yazmanız gerekecek
        }
    }

}
