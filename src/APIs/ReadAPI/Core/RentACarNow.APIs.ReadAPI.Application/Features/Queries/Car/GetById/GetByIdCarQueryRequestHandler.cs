using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
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
        private readonly ICustomCarCacheService _cacheService;
        public GetByIdCarQueryRequestHandler(
            IMongoCarReadRepository readRepository,
            ILogger<GetByIdCarQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<GetByIdCarQueryResponse> responseBuilder,
            ICustomCarCacheService cacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<GetByIdCarQueryResponse>> Handle(GetByIdCarQueryRequest request, CancellationToken cancellationToken)
        {
            var entity = _cacheService.GetEntity(request.CarId);

            if (entity is null)
            {
                entity = await _readRepository.GetByIdAsync(request.CarId);

                if (entity is null)
                    return _responseBuilder
                        .SetHttpStatusCode(HttpStatusCode.NotFound)
                        .Build();

            }

            _cacheService.SetEntity(entity.Id, entity, TimeSpan.FromMinutes(1));

            var responseData = _mapper.Map<GetByIdCarQueryResponse>(entity);


            return _responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();




        }
    }

}
