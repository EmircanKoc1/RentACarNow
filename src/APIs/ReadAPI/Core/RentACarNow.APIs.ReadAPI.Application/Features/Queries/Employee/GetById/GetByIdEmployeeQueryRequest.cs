using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Employee.GetById
{
    public class GetByIdEmployeeQueryRequest : IRequest<IEnumerable<GetByIdEmployeeQueryResponse>>
    {
        public Guid Id { get; set; }

    }

}
