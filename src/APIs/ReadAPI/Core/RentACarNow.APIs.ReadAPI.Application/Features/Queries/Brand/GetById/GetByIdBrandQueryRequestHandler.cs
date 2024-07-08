using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById
{


    public class GetByIdBrandQueryRequestHandler : IRequestHandler<GetByIdBrandQueryRequest, IEnumerable<GetByIdBrandQueryResponse>>
    {
        public Task<IEnumerable<GetByIdBrandQueryResponse>> Handle(GetByIdBrandQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
