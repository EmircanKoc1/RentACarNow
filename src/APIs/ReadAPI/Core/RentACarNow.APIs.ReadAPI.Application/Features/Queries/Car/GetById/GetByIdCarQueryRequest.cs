using MediatR;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById
{
    public class GetByIdCarQueryRequest : IRequest<ResponseWrapper<GetByIdCarQueryResponse>>
    {
        public Guid CarId { get; set; }

    }

}
