using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using RentACarNow.APIs.ReadAPI.Application.Interfaces.Services;
using RentACarNow.APIs.ReadAPI.Application.Wrappers;
using RentACarNow.Common.Infrastructure.Repositories.Interfaces.Read.Mongo;
using RentACarNow.Common.Models;
using System.Linq.Expressions;
using System.Net;
using mongoeEntity = RentACarNow.Common.MongoEntities;


namespace RentACarNow.APIs.ReadAPI.Application.Features.Queries.Claim.GetAll
{
    public class GetAllClaimQueryRequestHandler : IRequestHandler<GetAllClaimQueryRequest, ResponseWrapper<IEnumerable<GetAllClaimQueryResponse>>>
    {
        private readonly IMongoClaimReadRepository _readRepository;
        private readonly ILogger<GetAllClaimQueryRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ResponseBuilder<IEnumerable<GetAllClaimQueryResponse>> _responseBuilder;
        private readonly ICustomClaimCacheService _cacheService;

        public GetAllClaimQueryRequestHandler(
            IMongoClaimReadRepository repository,
            ILogger<GetAllClaimQueryRequestHandler> logger,
            IMapper mapper,
            ResponseBuilder<IEnumerable<GetAllClaimQueryResponse>> responseBuilder,
            ICustomClaimCacheService cacheService)
        {
            _readRepository = repository;
            _logger = logger;
            _mapper = mapper;
            _responseBuilder = responseBuilder;
            _cacheService = cacheService;
        }

        public async Task<ResponseWrapper<IEnumerable<GetAllClaimQueryResponse>>> Handle(GetAllClaimQueryRequest request, CancellationToken cancellationToken)
        {

            OrderingParameter? orderingParameter = default;

            if (request.SortingField is null)
                orderingParameter = OrderingParameter.CreateOrderingParameter(false, false, string.Empty);
            else
                orderingParameter = OrderingParameter.CreateOrderingParameter(true, request.IsAscending, request.SortingField);


            var paginationParameter = PaginationParameter.CreatePaginationParameter(request.PageNumber, request.PageSize);

            var claims = await _readRepository.GetAllAsync(
                paginationParameter: paginationParameter,
                filter: c => c.DeletedDate == null,
                orderingParameter: orderingParameter);

            long totalItemCount = 0;

            if (_cacheService.GetDbEntityCount() <= 0)
            {
                totalItemCount = await _readRepository.CountAsync();
                _cacheService.SetDbEntityCount(totalItemCount);
            }

            var paginationInfo = new PaginationInfo
            {
                PageItemSize = request.PageSize,
                PageNumber = request.PageNumber,
                TotalItemCount = totalItemCount
            };

            var getAllClaimQueryResponses = _mapper.Map<IEnumerable<GetAllClaimQueryResponse>>(claims);


            return _responseBuilder
                .SetData(getAllClaimQueryResponses)
                .SetHttpStatusCode(HttpStatusCode.OK)
                .SetPaginationInfo(paginationInfo)
                .Build();




        }
    }
}
