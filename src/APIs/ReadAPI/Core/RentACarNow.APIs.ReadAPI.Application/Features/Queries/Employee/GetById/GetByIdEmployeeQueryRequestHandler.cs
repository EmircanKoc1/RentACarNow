using MediatR;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetById
{
    public class GetByIdEmployeeQueryRequestHandler : IRequestHandler<GetByIdEmployeeQueryRequest, IEnumerable<GetByIdEmployeeQueryResponse>>
    {
        public Task<IEnumerable<GetByIdEmployeeQueryResponse>> Handle(GetByIdEmployeeQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
