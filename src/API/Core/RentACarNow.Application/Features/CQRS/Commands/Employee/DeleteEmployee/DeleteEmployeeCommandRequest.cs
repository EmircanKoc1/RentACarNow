using MediatR;

namespace RentACarNow.Application.Features.CQRS.Commands.Employee.DeleteEmployee
{
    public class DeleteEmployeeCommandRequest : IRequest<DeleteEmployeeCommandResponse>
    {
        public Guid Id { get; set; }

    }

}
