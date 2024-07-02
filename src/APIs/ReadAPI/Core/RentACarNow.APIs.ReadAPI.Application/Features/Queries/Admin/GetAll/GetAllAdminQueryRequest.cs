using FluentValidation;
using MediatR;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequest : IRequest<IEnumerable<GetAllAdminQueryResponse>>
    {

    }

    public class GetAllAdminQueryResponse
    {
        public Guid Id { get; set; }

    }


    public class GetAllAdminQueryRequestHandler : IRequestHandler<GetAllAdminQueryRequest, IEnumerable<GetAllAdminQueryResponse>>
    {
        IMongoAdminReadRepository _repository;

        public GetAllAdminQueryRequestHandler(IMongoAdminReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetAllAdminQueryResponse>> Handle(GetAllAdminQueryRequest request, CancellationToken cancellationToken)
        {

            throw new Exception();

        }
    }

    public class GetAllAdminQueryRequestValidator : AbstractValidator<GetAllAdminQueryRequest>
    {
        public GetAllAdminQueryRequestValidator()
        {


        }


    }

}
