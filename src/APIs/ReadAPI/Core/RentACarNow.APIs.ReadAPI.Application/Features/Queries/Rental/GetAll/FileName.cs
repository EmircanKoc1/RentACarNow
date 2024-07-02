using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Rental.GetAll
{
    public class GetAllRentalQueryRequest : IRequest<IEnumerable<GetAllRentalQueryResponse>>
    {

    }

    public class GetAllRentalQueryResponse
    {

    }

    public class GetAllRentalQueryRequestHandler : IRequestHandler<GetAllRentalQueryRequest, IEnumerable<GetAllRentalQueryResponse>>
    {
        public Task<IEnumerable<GetAllRentalQueryResponse>> Handle(GetAllRentalQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            // Burada Rental ile ilgili sorgu işleminin kodunu yazmanız gerekecek
        }
    }

    public class GetAllRentalQueryRequestValidator : AbstractValidator<GetAllRentalQueryRequest>
    {
        public GetAllRentalQueryRequestValidator()
        {
            // Burada Rental ile ilgili sorgu komutunun doğrulama kurallarını ekleyebilirsiniz
        }
    }

}
