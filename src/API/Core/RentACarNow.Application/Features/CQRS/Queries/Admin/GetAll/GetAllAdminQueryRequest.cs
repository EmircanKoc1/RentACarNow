using FluentValidation;
using MediatR;
using RentACarNow.Application.Interfaces.Repositories.Read.Mongo;

namespace RentACarNow.Application.Features.CQRS.Queries.Admin.GetAll
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

            return (await _repository.GetAllAsync(new Models.PaginationParameters(10, 0))).Select(x =>
            {
                return new GetAllAdminQueryResponse { Id = x.Id, };
            });

        }
    }

    public class GetAllAdminQueryRequestValidator : AbstractValidator<GetAllAdminQueryRequest>
    {
        public GetAllAdminQueryRequestValidator()
        {


        }


    }

}
