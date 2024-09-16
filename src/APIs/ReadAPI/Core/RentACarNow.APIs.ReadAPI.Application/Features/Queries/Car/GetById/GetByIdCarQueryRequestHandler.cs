using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetAll;
using RentACarNow.APIs.ReadAPI.Application.Features.Queries.Brand.GetById;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Car.GetById
{
    public class GetByIdCarQueryRequestHandler : IRequestHandler<GetByIdCarQueryRequest, ResponseWrapper<GetByIdCarQueryResponse>>
    {
        private readonly IMongoCarReadRepository _readRepository;
        private readonly ILogger<GetByIdCarQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdCarQueryResponse> _responseBuilder;

        public GetByIdCarQueryRequestHandler(
            IMongoCarReadRepository readRepository,
            ILogger<GetByIdCarQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<GetByIdCarQueryResponse> responseBuilder)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
        }

        public async Task<ResponseWrapper<GetByIdCarQueryResponse>> Handle(GetByIdCarQueryRequest request, CancellationToken cancellationToken)
        {
            var entity = await _readRepository.GetByIdAsync(request.CarId);

            //var responseBuilder = ResponseWrapper<GetByIdBrandQueryResponse>
            //    .Builder();

            var responseBuilder = _responseBuilder;

            if (entity is null)
            {
                return responseBuilder
                    .SetHttpStatusCode(HttpStatusCode.NotFound)
                    .Build();
            }

            var responseData = _mapper.Map<GetByIdCarQueryResponse>(entity);


            return responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();


        }
    }

}
