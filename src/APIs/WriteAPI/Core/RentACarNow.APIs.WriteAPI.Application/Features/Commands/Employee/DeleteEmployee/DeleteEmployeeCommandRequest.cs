using MediatR;

namespace RentACarNow.APIs.WriteAPI.Application.Features.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommandRequest : IRequest<DeleteEmployeeCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
