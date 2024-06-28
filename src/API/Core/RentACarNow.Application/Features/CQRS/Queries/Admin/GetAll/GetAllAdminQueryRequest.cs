using FluentValidation;
using MediatR;

namespace RentACarNow.Application.Features.CQRS.Queries.Admin.GetAll
{
    public class GetAllAdminQueryRequest : IRequest<IEnumerable<GetAllAdminQueryResponse>>
    {

    }

    public class GetAllAdminQueryResponse
    {

    }


    public class GetAllAdminQueryRequestHandler : IRequestHandler<GetAllAdminQueryRequest, IEnumerable<GetAllAdminQueryResponse>>
    {
        public Task<IEnumerable<GetAllAdminQueryResponse>> Handle(GetAllAdminQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetAllAdminQueryRequestValidator : AbstractValidator<GetAllAdminQueryRequest>
    {
        public GetAllAdminQueryRequestValidator()
        {
            

        }


    }

}
