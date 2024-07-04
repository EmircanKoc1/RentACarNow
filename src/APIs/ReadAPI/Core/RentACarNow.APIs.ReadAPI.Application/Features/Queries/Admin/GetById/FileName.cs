using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Admin.GetById
{
    public class GetByIdAdminQueryRequest : IRequest<IEnumerable<GetByIdAdminQueryResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetByIdAdminQueryResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

    }


    public class GetByIdAdminQueryRequestHandler : IRequestHandler<GetByIdAdminQueryRequest, IEnumerable<GetByIdAdminQueryResponse>>
    {
        public Task<IEnumerable<GetByIdAdminQueryResponse>> Handle(GetByIdAdminQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class GetByIdAdminQueryRequestValidator : AbstractValidator<GetByIdAdminQueryRequest>
    {
        public GetByIdAdminQueryRequestValidator()
        {


        }


    }
}
