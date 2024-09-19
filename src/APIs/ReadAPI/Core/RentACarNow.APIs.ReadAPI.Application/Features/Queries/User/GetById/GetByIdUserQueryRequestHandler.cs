using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using System.Net;

namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.User.GetById
{
    public class GetByIdUserQueryRequestHandler : IRequestHandler<GetByIdUserQueryRequest, ResponseWrapper<GetByIdUserQueryResponse>>
    {
        private readonly IMongoUserReadRepository _readRepository;
        private readonly ILogger<GetByIdUserQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<GetByIdUserQueryResponse> _responseBuilder;
        private readonly ICustomUserCacheService _cacheService;
        public GetByIdUserQueryRequestHandler(
            IMongoUserReadRepository readRepository,
            ILogger<GetByIdUserQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<GetByIdUserQueryResponse> responseBuilder,
            ICustomUserCacheService cacheService)
        {
            _readRepository = readRepository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<GetByIdUserQueryResponse>> Handle(GetByIdUserQueryRequest request, CancellationToken cancellationToken)
        {

            var entity = _cacheService.GetEntity(request.UserId);

            if (entity is null)
            {
                entity = await _readRepository.GetByIdAsync(request.UserId);

                if (entity is null)
                    return _responseBuilder
                        .SetHttpStatusCode(HttpStatusCode.NotFound)
                        .Build();

            }

            _cacheService.SetEntity(entity.Id, entity, TimeSpan.FromMinutes(1));

            var responseData = _mapper.Map<GetByIdUserQueryResponse>(entity);


            return _responseBuilder
                .SetData(responseData)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .Build();


        }
    }


}
