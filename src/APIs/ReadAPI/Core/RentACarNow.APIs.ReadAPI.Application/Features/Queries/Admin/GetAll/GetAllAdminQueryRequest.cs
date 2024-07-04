using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequest : IRequest<IEnumerable<GetAllAdminQueryResponse>>
    {




    }

}
